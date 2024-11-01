using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;

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

            builder.Services.Configure<RequestLocalizationOptions>(config =>
            {
                var cultures = Constants.AvailableLanguages
                                        .Select(langCode => new CultureInfo(langCode))
                                        .ToList();
                config.SupportedCultures = config.SupportedUICultures = cultures;
                config.DefaultRequestCulture = new RequestCulture(Constants.DefaultLangCode);
            });

            builder.Services.AddControllers();
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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication((o) =>
            {
                o.AddScheme<DefaultAuthenticationHandler>(ApiConstants.ApiAuthDefaultScheme, ApiConstants.ApiAuthDefaultScheme);
                o.DefaultAuthenticateScheme = ApiConstants.ApiAuthDefaultScheme;
                o.DefaultChallengeScheme = ApiConstants.ApiAuthDefaultScheme;
            });

            builder.Logging.AddDebug();
            builder.Logging.AddConsole();

            var app = builder.Build();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                ApplyCurrentCultureToResponseHeaders = true,
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //Add NetCore Log Provider
            Log.Providers.AddNetCore(app.Logger);

            app.Run();
        }
    }
}
