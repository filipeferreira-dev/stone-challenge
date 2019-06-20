using Application.ExternalServices;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Repositories;
using Infra.ExternalServices;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DI
{
    public static class DependencyInjectionConfiguration
    {
        public static void Setup(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICityService, CityService>();
            serviceCollection.AddTransient<IPostalCodeService, PostalCodeService>();
            serviceCollection.AddTransient<IWeatherService, WeatherService>();
            serviceCollection.AddTransient<ICityRepository, CityRepository>();
            serviceCollection.AddTransient<ICityTemperatureRepository, CityTemperatureRepository>();
        }
    }
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection SetupDependencyInjection(this IServiceCollection serviceColletion)
        {
            DependencyInjectionConfiguration.Setup(serviceColletion);
            return serviceColletion;
        }
    }
}
