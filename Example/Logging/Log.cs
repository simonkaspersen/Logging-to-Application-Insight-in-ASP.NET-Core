using System;
using Logging_to_Application_Insight_in_ASP.NET_Core.Logging;

public class Log : ILog
    {
        private readonly Action<string> _logDebug;
        private readonly Action<string, Exception> _logError;
        private readonly Action<string> _logInfo;
		private readonly Action<string, Exception> _logWarn;

        public Log(Action<string> logInfo, Action<string> logDebug, Action<string, Exception> logWarn, Action<string, Exception> logError)
        {
            _logInfo = logInfo;
            _logDebug = logDebug;
			_logWarn = logWarn;
            _logError = logError;

        }

        public void Info(string message)
        {
            _logInfo(message);
        }

        public void Debug(string message)
        {
            _logDebug(message);
        }

		public void Error(string message, Exception exception)
        {
			_logError(message, exception);
        }

		public void Warning(string message, Exception exception)
        {
			_logWarn(message, exception);
        }
    }