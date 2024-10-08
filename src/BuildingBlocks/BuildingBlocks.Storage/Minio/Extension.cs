using BuildingBlocks.Storage.Configs;
using BuildingBlocks.Storage.Interface;
using BuildingBlocks.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Storage.Minio
{
    public static class Extension
    {
        public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioConfig>(configuration.GetSection("Minio"));

            services.AddScoped<IStorageService, MinioService>();

            return services;
        }
    }
}
