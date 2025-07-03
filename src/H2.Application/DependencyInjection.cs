using H2.Application.Usecases;
using Microsoft.Extensions.DependencyInjection;

namespace H2.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<UploadSensorDataHandler>();
            services.AddScoped<UploadImageHandler>();
            return services;
        }
    }
}
