using Contract;
using Contract.Service;
using Contract.Service.EmailServices;
using Contract.Service.UserProvider;
using Entity.Models;
using Entity.Models.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Shared.ExtensionServices;
using System.Text;

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
        // UserProvider Configuration
        public static void ConfigureUserProvider(this IServiceCollection services) =>
            services.AddScoped<IUserProvider, UserProvider>();
        // JWT Configuration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidAudience = jwtSettings["validAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }


        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddEmailServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddTransient<IEmailService, EmailService>();
        }

        public static void RequiredEmaiConfiguration(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(
                op => op.SignIn.RequireConfirmedEmail = true);
            services.Configure<DataProtectionTokenProviderOptions>(op => op.TokenLifespan = TimeSpan.FromHours(1));
        }

    }
}
