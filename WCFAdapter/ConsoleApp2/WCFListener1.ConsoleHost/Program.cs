using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace WCFListener1
{
    class Program
    {
        public static ListenerStartup listenerStartup = null;

        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();

            listenerStartup = new ListenerStartup(configuration);

            var builder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(configuration)
                .UseIISIntegration()
                .ConfigureServices(listenerStartup.ConfigureServices)
                .Configure(listenerStartup.Configure);

            var host = builder.Build();
            host.Run();
        }
    }
}
