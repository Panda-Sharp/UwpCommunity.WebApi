using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UwpCommunity.WebApi.Auth;
using UwpCommunity.WebApi.Factories;

/// <summary>
/// Add a reference to Yugen.Toolkit.Standard.Data
///
/// How To Create a Migration
/// Select Startup Project: UwpCommunity.WebApi
/// Go To: Package Manager Console
/// Select: Default Project: UwpCommunity.Data
/// (Optional) Write: Remove-Migration
/// Write: Add-Migration {MigrationName}
/// 
/// Swagger: https://localhost:5001/swagger
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

            ServiceProviderFactory.RegisterServices(services, Configuration);

            services.AddAuthentication("DiscordAuthentication")
                .AddScheme<AuthenticationSchemeOptions, DiscordAuthenticationHandler>("DiscordAuthentication", null);

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                options.OperationFilter<AuthResponsesOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(_myAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
