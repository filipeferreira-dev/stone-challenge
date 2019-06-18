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
            serviceCollection.AddTransient<ICityRepository, CityRepository>();
        }
    }
}
