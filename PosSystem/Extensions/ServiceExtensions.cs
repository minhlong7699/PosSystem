using Contract;
using Contract.Service;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Shared.ExtensionServices;

namespace PosSystem.Extensions
{
    public static class ServiceExtensions
    {

        // CORS Configuration
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        // IIS Configuration
        public static void ConfigureIISIntegration(this IServiceCollection service) =>
            service.Configure<IISOptions>(option =>
            {
            });

        // RepositoryManager Configuration
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
             services.AddScoped<IRepositoryManager, RepositoryManager>();

        // ServiceManager Configuration
        public static void ConfigureServiceManager(this IServiceCollection services) =>
             services.AddScoped<IServiceManager, ServiceManager>();

        // DbContext Configuration
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("Db")));


        // UploadImage Configuration
        public static void ConfigureUploadImageService(this IServiceCollection services) =>
            services.AddScoped<IUploadImageService, UploadImageService>();
    }
}
