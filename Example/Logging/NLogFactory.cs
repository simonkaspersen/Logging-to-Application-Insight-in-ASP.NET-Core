using System;
using Microsoft.ApplicationInsights.NLogTarget;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Config;

#pragma warning disable 618

namespace Logging_to_Application_Insight_in_ASP.NET_Core.Logging {
    public class NLogFactory : ILogFactory
    {
        private readonly IConfiguration _configuration;

        public NLogFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            SetupLogger();
        }

        private void SetupLogger()
        {
            var config = new LoggingConfiguration();
            
            // You need this only if you did not define InstrumentationKey in ApplicationInsights.config or want to use different instrumentation key
            var applicationInsightKey = _configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            var target                = new ApplicationInsightsTarget {InstrumentationKey = applicationInsightKey};

//            var logfile = new NLog.Targets.FileTarget("logfile") 
//            { 
//                Layout   = "${longdate} ${logger} ${level} ${message} ${newline} ${exception} ${exception:format=Type}",
//                FileName = "${basedir}/logs/Demo.log"
//                
//            };
            
            var loggingRule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(loggingRule);
//            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);         
//            config.AddTarget(logfile);
                     
            LogManager.Configuration = config;
        }

        public ILog GetLogger(Type type)
        {
            var logger = LogManager.GetLogger(type.ToString());
            return new Log(logger.Info, logger.Debug, logger.Warn, logger.Error);
        }
    }
}