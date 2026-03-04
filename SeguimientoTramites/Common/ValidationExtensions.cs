using FluentValidation;

namespace SeguimientoTramites.Common;

public static class ValidationExtensions
{
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        return services;
    }
}
