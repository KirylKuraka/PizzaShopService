using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalAccountAPI.Extensions;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using MassTransit.Contracts;
using MassTransit.Contracts.TransferObjects;
using PersonalAccountAPI.MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PersonalAccountAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();

            services.AddControllers();
            services.ConfigureJWT(Configuration);

            services.ConfigureSwagger();

            ConfigureServicesMassTransit.ConfigureServices(services, Configuration, new MassTransitConfiguration
            {
                IsDebug = Configuration.GetSection("RabbitServer").GetValue<bool>("IsDebug"),
                ServiceName = "Accounts",
                Configurator = bus =>
                {
                    bus.AddConsumer<AccountConsumer>();
                    bus.AddRequestClient<AccountRequest>();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personal Account API");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
