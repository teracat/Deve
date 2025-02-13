using System.Net;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Deve.Logging;
using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Api.Auth;
using Deve.Api.Helpers;
using Deve.Api.Swagger;
using Deve.Api.DataSourceBuilder;

namespace Deve.Api
{
    public partial class Program
    {
        protected Program() { }

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

            // Rate Limiter: you might need to change the configurations in RateLimiterHelper
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = RateLimiterHelper.CreateRateLimiter();
                options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
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
                options.OperationFilter<AcceptLanguageHeaderParameter>();
                options.CustomSchemaIds(i => i.FullName);
            });

            // Inject HttpContextAccessor to the Controllers constructor
            builder.Services.AddHttpContextAccessor();

            // Inject IDataSourceBuilder to the Controllers constructor
            builder.Services.AddSingleton<IDataSourceBuilder, DataSourceBuilderFactory>();

            // Authentication
            // We register the TokenManagerJwt so we can use it as the Default Scheme (you should change the keys in the class TokenManagerJwt)
            // If you don't want to use Jwt, you can remove the next line and remove the referenced projecte Deve.Auth.Jwt
            // If none is defined here, the default TokenManagerCrypt will be used (you should change the keys used to encrypt in AuthConstants)
            TokenManagerFactory.Set(ApiConstants.AuthDefaultScheme, new TokenManagerJwt());
            builder.Services.AddAuthentication((o) =>
            {
                o.AddScheme<DefaultAuthenticationHandler>(ApiConstants.AuthDefaultScheme, ApiConstants.AuthDefaultScheme);
                o.DefaultAuthenticateScheme = ApiConstants.AuthDefaultScheme;
                o.DefaultChallengeScheme = ApiConstants.AuthDefaultScheme;
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