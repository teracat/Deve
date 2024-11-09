using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using Deve.Common.Api;

namespace Deve.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Uncomment the following lines and set the ConnectionString for your DataSource, if needed
            /*var config = new DataSourceConfig()
            {
                ConnectionString = ""
            };
            DataSourceFactory.SetConfig(config);*/

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Languages
            builder.Services.Configure<RequestLocalizationOptions>(config =>
            {
                var cultures = Constants.AvailableLanguages
                                        .Select(langCode => new CultureInfo(langCode))
                                        .ToList();
                config.SupportedCultures = config.SupportedUICultures = cultures;
                config.DefaultRequestCulture = new RequestCulture(Constants.DefaultLangCode);
            });

            // Rate Limiter: you might need to change the configuration
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Allow a maximum of 20 requests every minute
                // More info: https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
                options.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 20;
                    options.Window = TimeSpan.FromMinutes(1);
                });
            });

            // Controllers
            builder.Services.AddControllers(options =>
            {
                //Comment next line if you don't want to alter the Status Code
                options.Filters.Add<ResultResponseFilter>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Deve.Api", Version = "v1" });
                options.AddSecurityDefinition(ApiConstants.ApiAuthDefaultScheme, new OpenApiSecurityScheme
                {
                    Description = @$"Authorization header using the {ApiConstants.ApiAuthDefaultScheme} scheme.
                      Enter '{ApiConstants.ApiAuthDefaultScheme}' [space] and then your token in the text input below.
                      Example: '{ApiConstants.ApiAuthDefaultScheme} xxxxxxxxxx'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = ApiConstants.ApiAuthDefaultScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = ApiConstants.ApiAuthDefaultScheme
                            },
                            Scheme = ApiConstants.ApiAuthDefaultScheme,
                            Name = ApiConstants.ApiAuthDefaultScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                options.OperationFilter<AcceptLanguageHeaderParameter>();
                options.CustomSchemaIds(i => i.FullName);
            });

            // Inject HttpContextAccessor to the Controllers constructor
            builder.Services.AddHttpContextAccessor();

            // Authentication
            builder.Services.AddAuthentication((o) =>
            {
                o.AddScheme<DefaultAuthenticationHandler>(ApiConstants.ApiAuthDefaultScheme, ApiConstants.ApiAuthDefaultScheme);
                o.DefaultAuthenticateScheme = ApiConstants.ApiAuthDefaultScheme;
                o.DefaultChallengeScheme = ApiConstants.ApiAuthDefaultScheme;
            });

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
            // If you want to publish the Swagger, remove this condition
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Add custom Middlewares
            app.UseMiddleware<TooManyRequestsMiddleware>();

            // Rate Limiter
            app.UseRateLimiter();

            // Authorization
            app.UseAuthorization();

            // Map the controllers and require Rate Limiting using the policy name used before
            app.MapControllers()
               .RequireRateLimiting("fixed");

            // Add NetCore Log Provider
            Log.Providers.AddNetCore(app.Logger);

            // Run
            app.Run();
        }
    }
}
