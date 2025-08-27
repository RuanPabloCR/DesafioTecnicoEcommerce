using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace shared.messaging;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            configureConsumers?.Invoke(busConfig);

            busConfig.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqConnection = configuration.GetConnectionString("rabbitmq");

                if (string.IsNullOrWhiteSpace(rabbitMqConnection))
                    throw new InvalidOperationException("RabbitMQ string not found.");

                cfg.Host(new Uri(rabbitMqConnection));
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
