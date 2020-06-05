using System;
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
            if (services == null) throw new ArgumentException(nameof(services));

            return services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NetDevPackAppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddIdentityEntityFrameworkContextConfiguration(
            this IServiceCollection services, IConfiguration configuration, string migrationAssembly, string identityConnectionName = null)
        {
            if (services == null) throw new ArgumentException(nameof(services));
            if (configuration == null) throw new ArgumentException(nameof(configuration));
            if (string.IsNullOrEmpty(migrationAssembly)) throw new ArgumentException(nameof(migrationAssembly));

            return services.AddDbContext<NetDevPackAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(identityConnectionName ?? "IdentityConnection"),
                    b => b.MigrationsAssembly(migrationAssembly)));
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentException(nameof(app));

            return app.UseAuthentication()
                      .UseAuthorization();
        }
    }
}