using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using AidKit.BLL.Implementions;
using AidKit.BLL.Interfaces;
using AidKit.Service.Implemetions;
using AidKit.Service.Interfaces;

namespace AidKit.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис авторизации в контейнер.
        /// </summary>
        /// <param name="serviceCollection">Сервисы.</param>
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".WebApi";
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AidKit API",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);

                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IMedicineManager, MedicineManager>();
        }

        public static void ConfigureMinio(this IServiceCollection services, IConfiguration configuration)
        {
            var minioConfig = configuration.GetSection("Minio");
            services.AddScoped<IFileStorageService, MinioFileStorageService>((provider) =>
            {
                var endpoint = minioConfig["Endpoint"];
                var accessKey = minioConfig["AccessKey"];
                var secretKey = minioConfig["SecretKey"];
                var useHttps = Convert.ToBoolean(minioConfig["UseHttps"]);

                if (endpoint is null || accessKey is null || secretKey is null)
                {
                    throw new ArgumentNullException(nameof(minioConfig), "Одно или несколько значений конфигурации Minio равны null.");
                }

                return new MinioFileStorageService(endpoint, accessKey, secretKey, useHttps);
            });
        }
    }
}
