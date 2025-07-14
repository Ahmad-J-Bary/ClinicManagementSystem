using Clinic.Application.Contracts.Infrastructure;
using Clinic.Infrastructure.EmailService;
using Clinic.Infrastructure.LoggingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure
{
    /// <summary>
    /// Extension methods for registering infrastructure services in the dependency injection container.
    /// This follows the pattern from HR.LeaveManagement.Clean for clean service registration.
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        /// <summary>
        /// Registers all infrastructure services including email, logging, and external integrations.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="configuration">The configuration to read settings from.</param>
        /// <returns>The service collection for method chaining.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register email service
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService.EmailService>();

            // Register logging service
            services.AddTransient<ILoggingService, LoggingService.LoggingService>();

            // Register notification service
            services.AddTransient<INotificationService, NotificationService.NotificationService>();

            return services;
        }
    }
}

