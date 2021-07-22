using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace MassTransit.Contracts
{
    public static class ConfigureServicesMassTransit
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, MassTransitConfiguration massTransitConfiguration)
        {
            if (massTransitConfiguration == null || massTransitConfiguration.IsDebug)
            {
                return;
            }

            var rabbitMQ = configuration.GetSection("RabbitServer");
            var url = rabbitMQ.GetValue<string>("Url");
            var host = rabbitMQ.GetValue<string>("Host");
            var username = rabbitMQ.GetValue<string>("Username");
            var password = rabbitMQ.GetValue<string>("Password");

            if (rabbitMQ == null || string.IsNullOrEmpty(url) || string.IsNullOrEmpty(host))
            {
                throw new Exception("AppSetings does not contains data for RabbitMQ");
            }

            services.AddMassTransit(x =>
            {
                x.AddBus(busFactory =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                        {
                            configurator.Username(username);
                            configurator.Password(password);
                        });

                        cfg.ConfigureEndpoints(busFactory, SnakeCaseEndpointNameFormatter.Instance);
                    });

                    massTransitConfiguration.BusControl?.Invoke(bus, services.BuildServiceProvider());
                    return bus;
                });

                massTransitConfiguration.Configurator?.Invoke(x);

                services.AddMassTransitHostedService();
            });
        }
    }
}
