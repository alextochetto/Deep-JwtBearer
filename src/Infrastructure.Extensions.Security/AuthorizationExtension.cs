using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Security
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy("Trusted", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireRole("Admin");
                    builder.RequireClaim("Feature", "Create");
                });
                options.AddPolicy("CanCreate", builder =>
                {
                   builder.RequireAuthenticatedUser();
                   builder.RequireClaim("Feature", "Create"); 
                });
            });
        }
    }
}