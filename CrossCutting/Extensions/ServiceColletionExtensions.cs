using Microsoft.Extensions.DependencyInjection;
using Infra.DI;
namespace CrossCutting.Extensions
{
    public static class ServiceColletionExtensions
    {
        public static void SetupDependencyInjection(this IServiceCollection serviceColletion)
            => DependencyInjectionConfiguration.Setup(serviceColletion);
    }
}
