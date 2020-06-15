using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UwpCommunity.Data;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Services;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Services;
using Yugen.Toolkit.Standard.Data.Extensions;

namespace UwpCommunity.WebApi.Factories
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UwpCommunityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLite")))
                    .AddUnitOfWork<UwpCommunityDbContext>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILaunchService, LaunchService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IDiscordHttpClientService, DiscordHttpClientService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
