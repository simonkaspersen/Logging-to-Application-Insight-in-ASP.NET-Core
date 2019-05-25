using System;

namespace Logging_to_Application_Insight_in_ASP.NET_Core.Logging
{
    /// <summary>
    /// Log factory.
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>The logger.</returns>
        /// <param name="type">Type.</param>
        ILog GetLogger(Type type);
    }
}