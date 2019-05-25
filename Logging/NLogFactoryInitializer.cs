using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace Logging_to_Application_Insight_in_ASP.NET_Core.Logging {
    class NLogFactoryInitializer : ILogFactoryInitializer {
        public IWebHostBuilder UseLogger(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.UseNLog();
        }
    }
}