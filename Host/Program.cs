using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                                 .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();

            #region Read Config

            string environment = configuration.GetValue<string>("Api:Environment", Environments.Production);

            string httpUrl = configuration.GetValue<string>("Api:Kestrel:Http:Url");
            int httpPort = configuration.GetValue<int>("Api:Kestrel:Http:Port");

            #endregion

            var host = new WebHostBuilder()
                                            .UseConfiguration(configuration)
                                            .UseStartup<Startup>()
                                            .UseKestrel(options =>
                                            {
                                                options.AllowSynchronousIO = true;
                                                options.AddServerHeader = false;
                                                if (!string.IsNullOrEmpty(httpUrl) && httpPort != 0)
                                                {
                                                    options.Listen(IPAddress.Parse(httpUrl), httpPort);
                                                }
                                            })
                                            .UseEnvironment(environment)
                                            .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                                            .Build();

            host.Run();

        }
        #region Migration Entry Point

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion
    }
}