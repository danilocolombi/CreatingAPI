using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace CreatingAPI.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "CreatingOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Creating API",
                        Version = "1",
                        Description = "Through this API you can access unscrumbles and their information.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "daniloc.t@hotmail.com",
                            Name = "Danilo Colombi Tavares",
                            Url = new Uri("https://www.facebook.com/danilo.colombitavares")
                        }

                    });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            return services;
        }
    }
}
