using Microsoft.Extensions.DependencyInjection;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Core.Services;
using TrackMyShipment.Repository.Implementations;
using TrackMyShipment.Repository.Interfaces;

namespace TrackMyShipment.Core.RegisterServices
{
    public static partial class Services
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ICarrierService, CarrierService>()
                .AddTransient<ICarrierRepository, CarrierRepository>()
                .AddTransient<IAddressService, AddressService>()
                .AddTransient<IAddressRepository, AddressRepository>()
                .AddTransient<ICompanyService, CompanyService>()
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<ISubscriptionService, SubscriptionService>()
                .AddTransient<ISubscriptionRepository, SubscriptionsRepository>()
                .AddTransient<IObjectiveService, ObjectiveService>()
                .AddTransient<IObjectiveRepository, ObjectiveRepository>();

            return services;
        }
    }
}