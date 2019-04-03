using Microsoft.Extensions.DependencyInjection;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Core.Services;
using TrackMyShipment.Repository.Implemetations;
using TrackMyShipment.Repository.Interfaces;

namespace TrackMyShipment.Extension
{

    public static class Services
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddTransient<ICarrierService, CarrierService>()
            .AddTransient<ICarrierRepository, CarrierRepository>()
            .AddTransient<ICustomerService, CustomerService>()
            .AddTransient<ICustomerRepository, CustomerRepository>();
            

            return services;

        }
    }
}

