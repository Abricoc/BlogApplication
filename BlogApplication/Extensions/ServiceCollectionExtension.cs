using BlogApplication.Data;
using BlogApplication.Interceptors;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        return services.ConfigureDatabase(configuration)
            .ConfigureAuthorization()
            .ConfigureInterceptors();
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddEntityFrameworkSqlite()
            .AddDbContext<BlogContext>(options => options
                .UseSqlite(configuration.GetConnectionString("DefaultConnection")));
    }
    
    private static IServiceCollection ConfigureInterceptors(this IServiceCollection services)
    {
        return services
            .AddScoped<AuthInterceptor>()
            .AddScoped<PostInterceptor>()
            .AddScoped<CommentInterceptor>()
            .AddScoped<TagInterceptor>();
    }

    private static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            });
        
        return services;
    }
}