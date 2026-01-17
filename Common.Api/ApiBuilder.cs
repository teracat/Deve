using System.Globalization;
using System.Net;
//using Sentry.OpenTelemetry;
using Deve.Api.Auth;
using Deve.Api.Helpers;
using Deve.Api.Options;
using Deve.Api.Swagger;
using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Auth.UserIdentityService;
using Deve.Cache;
using Deve.Data;
using Deve.Diagnostics;
using Deve.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;

namespace Deve.Api;

public sealed class ApiBuilder
{
    private readonly List<ApiBuilderAppAction> _appActions = [];
    private readonly WebApplicationBuilder _builder;
    private readonly AppSettingsOptions _appSettings;

    public IServiceCollection Services => _builder.Services;

    /// <summary>
    /// Configures and starts the web API application using the specified command-line arguments.
    /// </summary>
    /// <remarks>This method sets up essential services and middleware for the API, including logging,
    /// authentication, localization, rate limiting, caching, diagnostics, and API documentation. It builds and runs
    /// the application, mapping controllers and enabling features such as Swagger UI and Prometheus metrics
    /// endpoint if configured. Call this method from your application's entry point to initialize and launch the
    /// API.</remarks>
    /// <param name="args">An array of command-line arguments to configure the application. Typically includes settings such as
    /// environment, URLs, or other startup options.</param>
    public static void CreateAndRun(string[] args)
    {
        Create(args)
            .Configure()
            .Build()
            .Run();
    }

    /// <summary>
    /// Creates a new instance of the ApiBuilder class using the specified command-line arguments.
    /// </summary>
    /// <param name="args">An array of command-line arguments to configure the API builder. Cannot be null.</param>
    /// <returns>An ApiBuilder instance initialized with the provided arguments.</returns>
    public static ApiBuilder Create(string[] args) => new(args);

    /// <summary>
    /// Initializes a new instance of the ApiBuilder class using the specified command-line arguments.
    /// </summary>
    /// <remarks>This constructor sets up logging and application configuration before the web
    /// application is built. Use this constructor to prepare the application environment with custom
    /// arguments.</remarks>
    /// <param name="args">An array of command-line arguments to configure the application builder. Cannot be null.</param>
    private ApiBuilder(string[] args)
    {
        // We add the console logger for the logs generated before the WebApplication is built.
        Log.Providers.AddConsole();

        // Create a new WebApplication builder with the provided arguments.
        _builder = WebApplication.CreateBuilder(args);

        // Create an instance of AppSettings to store configuration values.
        _appSettings = AddOptions();
    }

    public ApiBuilder Configure()
    {
        // Configure localization settings for the application.
        AddLocalization();

        // Configure request rate limiting for the application.
        AddRateLimiter();

        // Add API explorer support.
        AddSwagger();

        // Register the IHttpContextAccessor service, allowing access to the current HTTP context.
        _ = _builder.Services.AddHttpContextAccessor();

        // Configure authentication services with a custom authentication scheme.
        AddAuth();

        // Register some needed classes
        RegisterContextServices();

        // Add Cache
        string? redisServiceKey = AddCache();

        // Logging
        _ = _builder.Logging.AddDebug();
        _ = _builder.Logging.AddConsole();

        // Diagnostics
        AddDiagnostics(redisServiceKey);

        return this;
    }

    public WebApplication Build()
    {
        // Build
        var app = _builder.Build();

        foreach (var appAction in _appActions.OrderBy(x => x.Priority).ThenBy(x => x.Position))
        {
            appAction.Action(app);
        }

        // Add NetCore Log Provider
        Log.Providers.AddNetCore(app.Logger);
        Log.Providers.RemoveConsole(); // We remove the console logger because it's already included in the NetCore provider.

        return app;
    }

    /// <summary>
    /// Registers an application action to be executed with the specified priority during WebApplication
    /// configuration.
    /// </summary>
    /// <param name="action">The delegate that defines the action to perform on the WebApplication instance. Cannot be null.</param>
    /// <param name="priority">The priority that determines the order in which the action is executed relative to other registered actions.</param>
    private void AddAppAction(Action<WebApplication> action, ApiBuilderAppActionPriority priority) => _appActions.Add(new ApiBuilderAppAction(action, _appActions.Count, priority));

    /// <summary>
    /// Adds an action to be executed with the application's <see cref="WebApplication"/> instance during
    /// configuration. It will be executed with normal priority.
    /// </summary>
    /// <param name="action">The action to perform on the <see cref="WebApplication"/> instance. Cannot be null.</param>
    private void AddAppAction(Action<WebApplication> action) => AddAppAction(action, ApiBuilderAppActionPriority.Normal);

    /// <summary>
    /// Creates and configures an instance of the AppSettings class using values from the application's configuration.
    /// It also binds the ConnectionStringsOptions section to the corresponding options class.
    /// </summary>
    /// <returns>An AppSettings instance populated with values from the "AppSettings" section of the configuration.</returns>
    private AppSettingsOptions AddOptions()
    {
        _ = _builder.Services
                    .AddOptions<ConnectionStringsOptions>()
                    .Bind(_builder.Configuration.GetSection("ConnectionStrings"));

        var appSettings = new AppSettingsOptions();
        _builder.Configuration
                .GetSection("AppSettings")
                .Bind(appSettings);

        return appSettings;
    }

    /// <summary>
    /// Configures localization services for the application, including supported cultures and the default request culture.
    /// </summary>
    /// <remarks>This method sets up localization by specifying the application's supported cultures
    /// and UI cultures based on available language codes. It also configures the default request culture using the
    /// application's default language code. Call this method during application startup to enable localization
    /// features.</remarks>
    private void AddLocalization()
    {
        // Configures localization settings for the application.
        _ = _builder.Services.Configure<RequestLocalizationOptions>(config =>
        {
            // Creates a list of supported cultures from the available language codes.
            var cultures = Constants.AvailableLanguages
                                    .Select(langCode => new CultureInfo(langCode))
                                    .ToList();

            // Sets the supported cultures and UI cultures for localization.
            config.SupportedCultures = config.SupportedUICultures = cultures;

            // Sets the default request culture based on the application's default language code.
            config.DefaultRequestCulture = new RequestCulture(Constants.DefaultLangCode);
        });

        AddAppAction(app =>
        {
            // Request Localization
            _ = app.UseRequestLocalization(new RequestLocalizationOptions
            {
                ApplyCurrentCultureToResponseHeaders = true,
            });
        }, ApiBuilderAppActionPriority.High);
    }

    /// <summary>
    /// Configures rate limiting for the application by adding a global rate limiter to the service collection.
    /// </summary>
    /// <remarks>This method sets up a global rate limiter using a custom configuration. Requests that
    /// exceed the defined rate limits will receive an HTTP 429 (Too Many Requests) response.
    /// Adjust the rate limiter settings as needed to match application requirements.</remarks>
    private void AddRateLimiter()
    {
        // You might need to change the configurations in RateLimiterHelper.
        _ = _builder.Services.AddRateLimiter(options =>
        {
            // Sets a global rate limiter using a custom helper method.
            options.GlobalLimiter = RateLimiterHelper.CreateRateLimiter();

            // Defines the HTTP status code to return when a request is rejected due to exceeding the rate limit.
            options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
        });

        AddAppAction(app =>
        {
            // Add TooManyRequestsMiddleware
            _ = app.UseMiddleware<TooManyRequestsMiddleware>();

            // Rate Limiter
            _ = app.UseRateLimiter();
        }, ApiBuilderAppActionPriority.High);
    }

    /// <summary>
    /// Configures Swagger and OpenAPI services, enabling API
    /// documentation and interactive UI with security and language header support.
    /// </summary>
    /// <remarks>This method registers Swagger generation, sets up API key authentication for the
    /// Swagger UI, adds support for the "Accept-Language" header, and ensures unique schema IDs. It also configures
    /// document inclusion based on API versioning. Call this method during application startup to enable API
    /// documentation and testing features.</remarks>
    private void AddSwagger()
    {
        _ = _builder.Services.AddEndpointsApiExplorer();

        // Configures Swagger generation and security settings.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _ = _builder.Services.AddSwaggerGen(options =>
        {
            // Registers a new OpenAPI document (optional, if multiple versions are needed).
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Deve.Api v1", Version = "v1" });

            // Defines the authentication scheme for Swagger UI.
            options.AddSecurityDefinition(ApiConstants.AuthDefaultScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = ApiConstants.AuthDefaultScheme,
                In = ParameterLocation.Header,
                Description = @$"Authorization header using the {ApiConstants.AuthDefaultScheme} scheme.
                      Enter '{ApiConstants.AuthDefaultScheme}' [space] and then your token in the text input below.
                      Example: '{ApiConstants.AuthDefaultScheme} xxxxxxxxxx'",
            });

            // Requires the authentication scheme to be used in API requests.
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(ApiConstants.AuthDefaultScheme, document)] = []
            });

            // Adds a custom filter to include the "Accept-Language" header parameter in Swagger UI.
            options.OperationFilter<AcceptLanguageHeaderParameter>();

            // Ensures unique schema IDs by using the full class name.
            options.CustomSchemaIds(i => i.FullName);

            // Condition to include controllers in the corresponding Swagger document.
            options.DocInclusionPredicate((version, apiDesc) => apiDesc.RelativePath!.Contains($"{version}/", StringComparison.OrdinalIgnoreCase));
        });

        AddAppAction(app =>
        {
            // Swagger
            // If you don't want to publish the Swagger, uncomment this condition
            //if (app.Environment.IsDevelopment())
            //{
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(options =>
                {
                    // Adds a Swagger UI endpoint for the API documentation.
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Deve.Api v1");
                });
            //}
        });
    }

    /// <summary>
    /// Configures authorization & authentication services for the application, including registering a custom authentication scheme
    /// and setting up token management using JWT keys.
    /// </summary>
    /// <remarks>This method registers a custom authentication scheme and injects a token manager for
    /// handling JWT-based authentication. The signing and encryption secret keys in <paramref name="appSettings"/>
    /// must each be 32 characters, must not use default placeholder values, and must be different from each other.
    /// Update these keys in the application's configuration file before enabling authentication.</remarks>
    /// specify valid signing and encryption secret keys.</param>
    /// <exception cref="Exception">Thrown if the JWT keys in <paramref name="appSettings"/> are missing, invalid in length, set to default
    /// values, or if the signing and encryption keys are identical.</exception>
    private void AddAuth()
    {
        _ = _builder.Services.AddAuthentication((o) =>
        {
            // Adds a custom authentication scheme using DefaultAuthenticationHandler.
            o.AddScheme<DefaultAuthenticationHandler>(ApiConstants.AuthDefaultScheme, ApiConstants.AuthDefaultScheme);

            // Sets the default authentication scheme.
            o.DefaultAuthenticateScheme = ApiConstants.AuthDefaultScheme;

            // Sets the default challenge scheme.
            o.DefaultChallengeScheme = ApiConstants.AuthDefaultScheme;
        });

        // We register the TokenManagerJwt so we can use it as the Default Scheme (you should change the keys in the appsettings.json in the Api project).
        // If you don't want to use Jwt, you can remove the next lines and remove the referenced projecte Deve.Auth.Jwt.
        if (_appSettings.JwtKeys is null || Utils.SomeIsNullOrWhiteSpace(_appSettings.JwtKeys.SigningSecretKey, _appSettings.JwtKeys.EncryptionSecretKey))
        {
            throw new InvalidOperationException("The JwtKeys is empty. Please set the JwtKeys in the appsettings.json file.");
        }
        if (_appSettings.JwtKeys.SigningSecretKey.Length != 32 || _appSettings.JwtKeys.EncryptionSecretKey.Length != 32)
        {
            throw new InvalidOperationException("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must have 32 characters.");
        }
        if (_appSettings.JwtKeys.SigningSecretKey == "YouShouldChangeThisKey_MustBe32B" || _appSettings.JwtKeys.EncryptionSecretKey == "YouShouldChangeThisKey_MustBe32B")
        {
            throw new InvalidOperationException("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must be different from the default keys. Change them in appsettings.json.");
        }
        if (_appSettings.JwtKeys.SigningSecretKey == _appSettings.JwtKeys.EncryptionSecretKey)
        {
            throw new InvalidOperationException("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must be different.");
        }

        // Inject the TokenManager: used to generate and validate tokens.
        _ = _builder.Services.AddSingleton<ITokenManager, JwtTokenManager>((_) => new JwtTokenManager(_appSettings.JwtKeys.SigningSecretKey, _appSettings.JwtKeys.EncryptionSecretKey));

        // If you want to use TokenManagerCrypt with DataProtection, uncomment the following lines.
        //var dataProtectionProvider = Microsoft.AspNetCore.DataProtection.DataProtectionProvider.Create(nameof(Program));
        //var tokenManagerCrypt = new TokenManagerCrypt(new Crypt.CryptDataProtect(dataProtectionProvider), true);
        //builder.Services.AddSingleton<ITokenManager>(tokenManagerCrypt);
    }

    /// <summary>
    /// Registers http context services.
    /// </summary>
    private void RegisterContextServices()
    {
        // Registers DataOptionsFromContextAccessor as the implementation for IDataOptions with a scoped lifetime.
        // A new instance is created for each request, based on the current HTTP context.
        _ = _builder.Services.AddScoped<IDataOptions, HttpContextDataOptions>();

        // Registers ContextAccessorUserIdentityService as the implementation for IUserIdentityService with a scoped lifetime.
        // A new instance is created for each request, based on the current HTTP context.
        // This service is used to retrieve user identity information from the HTTP context.
        _ = _builder.Services.AddScoped<IUserIdentityService, HttpContextUserIdentityService>();
    }

    /// <summary>
    /// Registers a cache implementation for the ICache service in the application's dependency injection container,
    /// using RedisCache if a Redis connection string is configured.
    /// </summary>
    /// <remarks>If the RedisCacheConnection string is not set or is empty, a SimpleInMemoryCache is
    /// registered as the ICache implementation instead of RedisCache. The method should be called during
    /// application startup to ensure the appropriate cache service is available throughout the application's
    /// lifetime.</remarks>
    /// <returns>A RedisCache instance if the RedisCacheConnection string is set in the configuration; otherwise, null.</returns>
    private string? AddCache()
    {
        string? redisServiceKey = null;
        // Registers RedisCache as the implementation for ICache with singleton lifetime (only if the RedisCacheConnection is set in the appsettings.json file).
        // The RedisCacheConnection string is retrieved from the configuration file.
        // The RedisCache class is defined in the Extra/Cache.Redis project (you can remove it if you don't use it).
        var redisConnection = _builder.Configuration.GetConnectionString("RedisCacheConnection") ?? string.Empty;
        if (string.IsNullOrWhiteSpace(redisConnection))
        {
            Log.Debug("The RedisCacheConnection is empty. Using SimpleInMemoryCache as the ICache implementation.");

            // Registers SimpleInMemoryCache as the implementation for ICache with singleton lifetime.
            // It will use the default expiration time and the default cleanup interval defined in the SimpleInMemoryCache class. Change these values as needed.
            _ = _builder.Services.AddSingleton<ICache, SimpleInMemoryCache>();

            // Registers InMemoryCache as the implementation for ICache with singleton lifetime.
            // The cache is configured with a 5-minute expiration scan frequency.
            // This is an alternative and it's included in the Extra/Cache.Memory project (you should add it if you want to use this implementation).
            //builder.Services.AddSingleton<ICache>(new InMemoryCache(new MemoryCacheOptions()
            //{
            //    ExpirationScanFrequency = TimeSpan.FromMinutes(5),
            //}));

            // If you want to throw an exception when the RedisCacheConnection is not set, uncomment the next line.
            //throw new Exception("The RedisCacheConnection is empty. Please set the RedisCacheConnection in the appsettings.json file.");
        }
        else
        {
            Log.Debug("The RedisCacheConnection is set. Using RedisCache as the ICache implementation.");

            redisServiceKey = "RedisCache";

            _ = _builder.Services.AddKeyedSingleton(typeof(ICache), redisServiceKey, (_, _) => new RedisCache(redisConnection));
        }
        return redisServiceKey;
    }

    /// <summary>
    /// Configures diagnostics, including OpenTelemetry integration and optional Redis cache instrumentation.
    /// </summary>
    /// <remarks>To enable OpenTelemetry diagnostics, ensure the required packages are referenced.
    /// For Sentry integration, additional configuration and packages may be necessary.</remarks>
    /// <param name="redisServiceKey">An optional Redis cache service key that will be used to get the redis connection from the services for diagnostics, if provided.
    /// If null, Redis diagnostics will not be configured.</param>
    private void AddDiagnostics(string? redisServiceKey)
    {
        // OpenTelemetry - if you don't want to use OpenTelemetry, remove the project Deve.Diagnostics.OpenTelemetry.AspNetCore as a reference and comment the next lines.
        // To use OpenTelemetry with Sentry, you need to add the Sentry.OpenTelemetry package and the line "options.UseOpenTelemetry();" in the SentryOptionsExtensions class in the Diagnostics.Sentry project.
        _ = _builder.AddDiagnosticsOpenTelemetry(redisServiceKey,
            funcConfigMetrics: (metrics) =>
            {
                // Configure extra Metrics exporters here (if you want to use other exporters).
            },
            funcConfigTracing: (tracing) =>
            {
                // Configure Tracing exporters here (if you want to use other exporters).

                // Example for Sentry:
                //tracing.AddSentry();    // You need to uncomment the "using Sentry.OpenTelemetry;" line at the top of this file.
            });

        // Sentry - if you want to use Sentry, add the project Deve.Diagnostics.Sentry.AspNetCore as a reference, uncomment the next lines and define your DSN in the appsettings.json.
        // You should change the DSN with your own (you can create a free account at https://sentry.io/welcome/)
        // Log.Providers.AddSentry is not needed, Sentry captures automatically the logs from ASP.NET Core.
        //builder.WebHost.AddDiagnosticsSentry();

        AddAppAction(app =>
        {
            // Prometheus: adds the Prometheus scraping endpoint at /metrics (to be used with OpenTelemetry).
            if (!string.IsNullOrWhiteSpace(_builder.Configuration["PROMETHEUS_SCRAPE_ENDPOINT"]))
            {
                _ = app.MapPrometheusScrapingEndpoint()
                       .DisableHttpMetrics();   // We don't want metrics about the /metrics endpoint (https://github.com/dotnet/aspnetcore/issues/50654).
            }
        });
    }
}
