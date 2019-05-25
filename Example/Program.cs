using LightInject;
using LightInject.Microsoft.AspNetCore.Hosting;
using Logging_to_Application_Insight_in_ASP.NET_Core.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace Logging_to_Application_Insight_in_ASP.NET_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new ServiceContainer();
            var loggerFactoryInitializer = container.GetInstance<ILogFactoryInitializer>();
            var webhostBuilder = CreateWebHostBuilder(args);
            loggerFactoryInitializer.UseLogger(webhostBuilder);
            
            webhostBuilder.Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseLightInject()
                   .UseApplicationInsights()
                   .UseStartup<Startup>();
    }
}