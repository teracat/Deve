using System.Net;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Deve.Logging;
using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;
using Deve.Auth;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Core;
using Deve.Api.Auth;
using Deve.Api.Helpers;
using Deve.Api.Swagger;
using Deve.Api.Settings;

namespace Deve.Api
{
    public partial class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            // Creates a new WebApplication builder with the provided arguments.
            var builder = WebApplication.CreateBuilder(args);

            // Creates an instance of AppSettings to store configuration values.
            var appSettings = new AppSettings();

            // Binds the "AppSettings" section from the configuration file to the appSettings instance.
            builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

            // Uncomment the following lines and set the DefaultConnection in appsettings.json, if needed
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("The ConnectionString is empty. Please set the ConnectionString in the appsettings.json file.");
            }
            var dsConfig = new DataSourceConfig(connectionString);

            // Configures localization settings for the application.
            builder.Services.Configure<RequestLocalizationOptions>(config =>
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

            // Configures request rate limiting for the application.
            // You might need to change the configurations in RateLimiterHelper.
            builder.Services.AddRateLimiter(options =>
            {
                // Sets a global rate limiter using a custom helper method.
                options.GlobalLimiter = RateLimiterHelper.CreateRateLimiter();

                // Defines the HTTP status code to return when a request is rejected due to exceeding the rate limit.
                options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
            });

            // Adds controller support to the application.
            builder.Services.AddControllers(options =>
            {
                // Comment the next line if you don't want to modify the response status code.
                options.Filters.Add<ResultResponseFilter>();
            });


            // Adds API explorer support.
            builder.Services.AddEndpointsApiExplorer();

            // Configures Swagger generation and security settings.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen(options =>
            {
                // Registers a new OpenAPI document (optional, if multiple versions are needed).
                //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Deve.Api", Version = "v1" });

                // Defines the authentication scheme for Swagger UI.
                options.AddSecurityDefinition(ApiConstants.AuthDefaultScheme, new OpenApiSecurityScheme
                {
                    Description = @$"Authorization header using the {ApiConstants.AuthDefaultScheme} scheme.
                      Enter '{ApiConstants.AuthDefaultScheme}' [space] and then your token in the text input below.
                      Example: '{ApiConstants.AuthDefaultScheme} xxxxxxxxxx'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = ApiConstants.AuthDefaultScheme
                });

                // Requires the authentication scheme to be used in API requests.
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = ApiConstants.AuthDefaultScheme
                            },
                            Scheme = ApiConstants.AuthDefaultScheme,
                            Name = ApiConstants.AuthDefaultScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Adds a custom filter to include the "Accept-Language" header parameter in Swagger UI.
                options.OperationFilter<AcceptLanguageHeaderParameter>();

                // Ensures unique schema IDs by using the full class name.
                options.CustomSchemaIds(i => i.FullName);
            });

            // Registers the IHttpContextAccessor service, allowing access to the current HTTP context.
            builder.Services.AddHttpContextAccessor();

            // Configures authentication services with a custom authentication scheme.
            builder.Services.AddAuthentication((o) =>
            {
                // Adds a custom authentication scheme using DefaultAuthenticationHandler.
                o.AddScheme<DefaultAuthenticationHandler>(ApiConstants.AuthDefaultScheme, ApiConstants.AuthDefaultScheme);

                // Sets the default authentication scheme.
                o.DefaultAuthenticateScheme = ApiConstants.AuthDefaultScheme;

                // Sets the default challenge scheme.
                o.DefaultChallengeScheme = ApiConstants.AuthDefaultScheme;
            });

            // We register the TokenManagerJwt so we can use it as the Default Scheme (you should change the keys in the appsettings.json in the External.Api & Internal.Api projects).
            // If you don't want to use Jwt, you can remove the next lines and remove the referenced projecte Deve.Auth.Jwt.
            if (appSettings.JwtKeys is null || Utils.SomeIsNullOrWhiteSpace(appSettings.JwtKeys.SigningSecretKey, appSettings.JwtKeys.EncryptionSecretKey))
            {
                throw new Exception("The JwtKeys is empty. Please set the JwtKeys in the appsettings.json file.");
            }
            if (appSettings.JwtKeys.SigningSecretKey.Length != 32 || appSettings.JwtKeys.EncryptionSecretKey.Length != 32)
            {
                throw new Exception("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must have 32 characters.");
            }
            if (appSettings.JwtKeys.SigningSecretKey == "YouShouldChangeThisKey_MustBe32B" || appSettings.JwtKeys.EncryptionSecretKey == "YouShouldChangeThisKey_MustBe32B")
            {
                throw new Exception("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must be different from the default keys. Change them in appsettings.json.");
            }
            if (appSettings.JwtKeys.SigningSecretKey == appSettings.JwtKeys.EncryptionSecretKey)
            {
                throw new Exception("The JwtKeys are not valid. The SigningSecretKey and EncryptionSecretKey must be different.");
            }

            // Inject the TokenManager: used to generate and validate tokens.
            var tokenManagerJwt = new TokenManagerJwt(appSettings.JwtKeys.SigningSecretKey, appSettings.JwtKeys.EncryptionSecretKey);
            builder.Services.AddSingleton<ITokenManager>(tokenManagerJwt);

            // If you want to use TokenManagerCrypt with DataProtection, uncomment the following lines.
            //var dataProtectionProvider = Microsoft.AspNetCore.DataProtection.DataProtectionProvider.Create(nameof(Program));
            //var tokenManagerCrypt = new TokenManagerCrypt(new Crypt.CryptDataProtect(dataProtectionProvider), true);
            //builder.Services.AddSingleton<ITokenManager>(tokenManagerCrypt);

            // Registers HashSha512 as the implementation for IHash with singleton lifetime.
            // A single instance is created and shared across the entire application.
            builder.Services.AddSingleton<IHash, HashSha512>();

            // Registers the provided instance of IDataSourceConfig as a singleton service.
            // The same instance (dsConfig) will be shared across the entire application.
            builder.Services.AddSingleton<IDataSourceConfig>(dsConfig);

            // Registers DataSourceMain as the implementation for IDataSource with a scoped lifetime.
            // A new instance is created per request.
            builder.Services.AddScoped<IDataSource, DataSourceMain>();

            // Registers DataOptionsFromContextAccessor as the implementation for IDataOptions with a scoped lifetime.
            // A new instance is created for each request, based on the current HTTP context.
            builder.Services.AddScoped<IDataOptions, DataOptionsFromContextAccessor>();

            // Registers AuthMain as the implementation for IAuth with a scoped lifetime.
            // A new instance is created per request to handle authentication operations.
            builder.Services.AddScoped<IAuth, AuthMain>();

            // Registers CoreMain as the implementation for ICore with scoped lifetime.
            // A new instance is created per request.
            builder.Services.AddScoped<ICore, CoreMain>();

            // Logging
            builder.Logging.AddDebug();
            builder.Logging.AddConsole();

            // Build
            var app = builder.Build();

            // Request Localization
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                ApplyCurrentCultureToResponseHeaders = true,
            });

            // Swagger
            // If you don't want to publish the Swagger, uncomment this condition
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add TooManyRequestsMiddleware
            app.UseMiddleware<TooManyRequestsMiddleware>();

            // Rate Limiter
            app.UseRateLimiter();

            // Authorization
            app.UseAuthorization();

            // Map the controllers
            app.MapControllers();

            // Add NetCore Log Provider
            Log.Providers.AddNetCore(app.Logger);

            // Run
            app.Run();
        }
    }
}