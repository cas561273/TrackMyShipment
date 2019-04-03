using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TrackMyShipment.Utils
{
    public static class JwtOptions
    {
        public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, string defaultScheme)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = AuthOptions.ValidateIssuer,
                  ValidateAudience = AuthOptions.ValidateAudience,
                  ValidateLifetime = AuthOptions.ValidateLifetime,
                  ValidateIssuerSigningKey = AuthOptions.ValidateIssuerSigningKey,
                  ValidIssuer = AuthOptions.ValidIssuer,
                  ValidAudience = AuthOptions.ValidAudience,
                  IssuerSigningKey = AuthOptions.IssuerSigningKey,
              };
          });
            return services;

        }
    }
}
