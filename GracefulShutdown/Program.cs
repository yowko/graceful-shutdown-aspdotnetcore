using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GracefulShutdown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            Console.WriteLine("application start");
            var life = host.Services.GetRequiredService<IHostApplicationLifetime>();
            
            life.ApplicationStopped.Register( () => {
                Console.WriteLine("Application is shut down");
                // long time works
                var _service = host.Services.GetRequiredService<SlowHostedService>();
                _service.StopAsync(new CancellationToken()).GetAwaiter().GetResult();
                //也可以直接指定等待時間
                //Thread.Sleep(10_1000);
            });
            host.Run();
            Console.WriteLine("application stop");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}