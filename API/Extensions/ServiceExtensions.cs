using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using SatelliteEmergencyHub.Application.Services.Implementations;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Implementations;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;
using System.Text;


namespace SatelliteEmergencyHub.API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";


            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IOccurrenceRepository, OccurrenceRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IEmergencyTeamRepository, EmergencyTeamRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IOccurrenceService, OccurrenceService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IEmergencyTeamService, EmergencyTeamService>();

            return services;
        }


        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
                ?? throw new InvalidOperationException("JWT_KEY environment variable is not set.");

            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                ?? throw new InvalidOperationException("JWT_ISSUER environment variable is not set.");


            var key = Encoding.UTF8.GetBytes(jwtKey!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return services;
        }


        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Satellite Emergency Hub API",
                    Version = "v1",
                    Description = "Disaster management platform monitored by satellite data."
                });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearer", document)] = []
                });
            });

            return services;
        }
    }
}
