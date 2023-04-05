using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

[ExcludeFromCodeCoverage(Justification = "This class is just used for service registration.")]
public static class DependencyInjection
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddMediatR(c => { c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly); });
    }
}