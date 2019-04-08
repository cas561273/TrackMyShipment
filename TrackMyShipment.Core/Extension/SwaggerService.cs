using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;


namespace TrackMyShipment.Core.Extension
{
    public partial class Services
    {
        public static IServiceCollection RegisterSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                Dictionary<string, IEnumerable<string>> security = new Dictionary<string, IEnumerable<string>>
                    {
                    {"Bearer", new string[] { }},
                    };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            return services;
        }
    }
}