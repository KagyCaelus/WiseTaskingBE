using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using Application.Services.Repositories;
using Data.Models;
using Data.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WiseTaskingBE.DependencyInjection;
public static class ServicesConfiguration {
    public static IServiceCollection Services(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WiseTaskingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetSection("ConnectionStrings:DeveloperConnection").Value);
        });

        services.AddCors(option => {
            option.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        AuthConfiguration authConfiguration = new AuthConfiguration(); 

        configuration.Bind("Authentication", authConfiguration);

        services.AddSingleton(authConfiguration);
        services.AddScoped<IPasswordHasher, BcriptPasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IWorkspaceService, WorkspaceService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key)),
                ValidIssuer = authConfiguration.Issuer,
                ValidAudience = authConfiguration.Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}
