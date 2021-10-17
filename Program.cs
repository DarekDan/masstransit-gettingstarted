using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, log) =>
                {
                    if (ctx.HostingEnvironment.IsProduction())
                    {
                        log.MinimumLevel.Warning();
                    }
                    else
                    {
                        log.MinimumLevel.Information();
                    }

                    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
                    log.WriteTo.Console();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();
                        x.AddConsumer<EvaConsumer>(configurator =>
                        {
                            configurator.UseMessageRetry(retryConfigurator =>
                                retryConfigurator.Interval(100, TimeSpan.FromMilliseconds(50)));
                        });
                        x.AddConsumer<BinaryMessageConsumer>();

                        x.SetKebabCaseEndpointNameFormatter();

                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(new Uri("amqp://127.0.0.1:5672/"), rabbitMqHostConfigurator =>
                            {
                                rabbitMqHostConfigurator.Username("guest");
                                rabbitMqHostConfigurator.Password("guest");
                                rabbitMqHostConfigurator.RequestedConnectionTimeout(TimeSpan.FromMilliseconds(500));
                                rabbitMqHostConfigurator.Heartbeat(TimeSpan.FromSeconds(15));
                            });

                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                    services.AddMassTransitHostedService(true);

                    services.AddHostedService<Worker>();
                });
    }
}