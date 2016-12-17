using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;
using WouterFennis.ChatApp.BackEnd.DAL;
using WouterFennis.ChatApp.BackEnd.DAL.Repositories;
using WouterFennis.ChatApp.BackEnd.Domain;
using WouterFennis.ChatApp.BackEnd.Managers;
using Swashbuckle.Swagger.Model;

namespace WouterFennis.ChatApp.BackEnd
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            // Setup database with docker connectionstring
            var dockerConnectionString = Environment.GetEnvironmentVariable("dbconnectionstring");
            services.AddDbContext<ChatRoomContext>
            (
                options => options.UseMySQL(dockerConnectionString)
            );

            // DI
            services.AddScoped<IRepository<ChatRoom, long>, ChatRoomRepository>();
            services.AddScoped<IChatRoomManager, ChatRoomManager>();

            // Add swagger
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "ChatApp Service",
                    Description = "ChatpApp Service",
                    TermsOfService = "None"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            // Use swagger
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
