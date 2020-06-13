using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UwpCommunity.Data;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Services;
using Yugen.Toolkit.Standard.Data.Extensions;

/// <summary>
/// Add a reference to Yugen.Toolkit.Standard.Data
///
/// How To Create a Migration
/// Select Startup Project: UwpCommunity.WebApi
/// Go To: Package Manager Console
/// Select: Default Project: UwpCommunity.Data
/// (Optional) Write: Remove-Migration
/// Write: Add-Migration {MigrationName}
/// </summary>
namespace UwpCommunity.WebApi
{
    public class Startup
    {
        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyOrigin();
                                  });
            });

            services.AddControllers();

            services.AddDbContext<UwpCommunityDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SQLite")))
                    .AddUnitOfWork<UwpCommunityDbContext>();

            services.AddTransient<ILaunchService, LaunchService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_myAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
