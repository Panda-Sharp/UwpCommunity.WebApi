using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Neleus.DependencyInjection.Extensions;
using System;
using UwpCommunity.Data;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Services;
using UwpCommunity.WebApi.Auth;
using UwpCommunity.WebApi.BotCommands;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;
using UwpCommunity.WebApi.Models.Discord;
using UwpCommunity.WebApi.Services;
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
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddDbContext<UwpCommunityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Postgre")))
                    .AddUnitOfWork<UwpCommunityDbContext>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILaunchService, LaunchService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<IDiscordHttpClientService, DiscordHttpClientService>();

            var discordSettings = Configuration.GetSection("Discord").Get<DiscordSettings>();
            services.AddSingleton<IDiscordBotService>(ctx =>
            {
                var discordBotCommandFactory = ctx.GetService<IServiceByNameFactory<IBotCommand>>();
                return new DiscordBotService(discordSettings, discordBotCommandFactory);
            });

            services.AddSingleton<PingBotCommand>();
            services.AddSingleton<UserBotCommand>();
            services.AddSingleton<NewsBotCommand>();
            services.AddSingleton<RoleInfoBotCommand>();

            services.AddByName<IBotCommand>()
                .Add<NewsBotCommand>("nnews")
                .Add<PingBotCommand>("nping")
                .Add<RoleInfoBotCommand>("nroleinfo")
                .Add<UserBotCommand>("ngetuser")
                .Build();

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
