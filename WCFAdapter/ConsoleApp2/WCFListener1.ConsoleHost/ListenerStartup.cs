using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WCFListener1
{
    public class ListenerStartup
    {
        public IConfiguration Configuration { get; }

        public ListenerStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ListenerStartup.ConfigureServices Start");

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
            services.AddCors();

            Console.WriteLine("ListenerStartup.ConfigureServices End");
        }

        public void Configure(IApplicationBuilder app)
        {
            Console.WriteLine("Adapter.Configure Start");

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute("Listener", "Listener", defaults: new { controller = "Listener", action = "GetSourceId" });
            });

            Console.WriteLine("Adapter.Configure End");
        }
    }
}
