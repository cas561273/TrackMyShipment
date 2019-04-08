using Microsoft.Extensions.DependencyInjection;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Core.Services;
using TrackMyShipment.Repository.Implementations;
using TrackMyShipment.Repository.Interfaces;

namespace TrackMyShipment.Core.Extension
{
    public static partial class Services
    {
        public static IServiceCollection RegisterJwtServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ICarrierService, CarrierService>()
                .AddTransient<ICarrierRepository, CarrierRepository>()
                .AddTransient<ICustomerService, CustomerService>()
                .AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}