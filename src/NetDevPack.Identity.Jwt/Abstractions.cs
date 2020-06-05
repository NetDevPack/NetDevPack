using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity.Data;

namespace NetDevPack.Identity
{
    public static class Abstractions
    {
        public static IdentityBuilder AddIdentityConfiguration(this IServiceCollection services)
        {
            return services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NetDevPackAppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddIdentityEntityFrameworkContextConfiguration(
            this IServiceCollection services, IConfiguration configuration, string migrationAssembly, string identityConnectionName = null)
        {
            return services.AddDbContext<NetDevPackAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(identityConnectionName ?? "IdentityConnection"),
                    b => b.MigrationsAssembly(migrationAssembly)));
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
        {
            return app.UseAuthentication()
                      .UseAuthorization();
        }
    }
}