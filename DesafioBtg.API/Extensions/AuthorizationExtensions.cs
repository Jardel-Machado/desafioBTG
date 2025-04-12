namespace DesafioBtg.API.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationExtensions(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminGlobalPolicy", policy =>
                policy.RequireClaim("AdminGlobal", "true"));
        });

        return services;
    }
}
