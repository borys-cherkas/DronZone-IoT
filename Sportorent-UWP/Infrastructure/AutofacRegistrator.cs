using Autofac;
using DronZone_IoT.Business.Services;
using DronZone_IoT.Business.Services.Implementations;
using DronZone_IoT.Data.Api.APIs;
using DronZone_IoT.Data.Api.APIs.Implementations;
using DronZone_IoT.Presentation.ViewModels;
using DronZone_IoT.Presentation.ViewModels.Auth;
using DronZone_IoT.Presentation.ViewModels.Drone;
using DronZone_IoT.Utils;

namespace DronZone_IoT.Infrastructure
{
    public static class AutofacRegistrator
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterApis(builder);
            RegisterViewModels(builder);
            RegisterUtils(builder);
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<LoginViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<MenuContentViewModel>().AsSelf().AsImplementedInterfaces();
            
            builder.RegisterType<UserDroneListViewModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<DroneDetailsViewModel>().AsSelf().AsImplementedInterfaces();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PreferencesService>().As<IPreferencesService>();
            builder.RegisterType<NetworkService>().As<INetworkService>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DroneService>().As<IDroneService>();
            builder.RegisterType<AreaService>().As<IAreaService>();
            builder.RegisterType<AreaFilterService>().As<IAreaFilterService>();
            builder.RegisterType<FlightService>().As<IFlightService>();
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<AuthRestApi>().As<IAuthRestApi>();
            builder.RegisterType<DroneRestApi>().As<IDroneRestApi>();
            builder.RegisterType<AreaRestApi>().As<IAreaRestApi>();
            builder.RegisterType<FilterRestApi>().As<IFilterRestApi>();
            builder.RegisterType<FlightRestApi>().As<IFlightRestApi>();
        }

        private static void RegisterUtils(ContainerBuilder builder)
        {
            builder.RegisterType<MenuNavigationHelper>().AsSelf().SingleInstance();
        }
    }
}
