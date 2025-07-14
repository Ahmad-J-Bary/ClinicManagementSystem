using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Application
{
    /// <summary>
    /// Extension methods for registering application services in the dependency injection container.
    /// This follows the pattern from HR.LeaveManagement.Clean for clean service registration.
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Registers all application services including MediatR, AutoMapper, and FluentValidation.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The service collection for method chaining.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register AutoMapper with all profiles from this assembly
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register MediatR with all handlers from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register FluentValidation validators from this assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register MediatR pipeline behaviors for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}


