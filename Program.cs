using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();
                        x.AddConsumer<BinaryMessageConsumer>();
                        
                        x.SetKebabCaseEndpointNameFormatter();
                        
                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(new Uri("amqp://guest:guest@localhost:5672"));
                            
                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                    services.AddMassTransitHostedService(true);
                    
                    services.AddHostedService<Worker>();
                });
    }
}
