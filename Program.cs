using System;
using GettingStarted.PaymentsDomain;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
                    var env = ctx.HostingEnvironment;

                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .Build();
                    log.ReadFrom.Configuration(configuration);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PaymentRequestConsumer>(configurator =>
                        {
                            configurator.UseMessageRetry(retryConfigurator =>
                                retryConfigurator.Interval(100, TimeSpan.FromMilliseconds(50)));
                        });

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