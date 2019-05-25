using Microsoft.AspNetCore.Hosting;

namespace Logging_to_Application_Insight_in_ASP.NET_Core.Logging
{
    public interface ILogFactoryInitializer
    {
        IWebHostBuilder UseLogger(IWebHostBuilder webHostBuilder);
    }
}