using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransit.Contracts
{
    public static class MassTransitExtension
    {
        public static void Configure(this IServiceCollection services, Action<MassTransitConfiguration> configuration, string serviceName)
        {
            var transitConfiguration = new MassTransitConfiguration();
            if (configuration == null)
            {
                throw new Exception(nameof(configuration));
            }

            configuration(transitConfiguration);

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new Exception(transitConfiguration.ServiceName);
            }

            transitConfiguration.ServiceName = serviceName;
            services.AddSingleton(transitConfiguration);
        }
    }
}
