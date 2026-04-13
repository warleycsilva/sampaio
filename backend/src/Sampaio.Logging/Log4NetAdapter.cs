using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Sampaio.Domain.Contracts.Infra;

namespace Sampaio.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly log4net.ILog _log;

        public Log4NetAdapter(string loggerName)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));            
            _log = LogManager.GetLogger(Assembly.GetEntryAssembly(), loggerName);             
        }

        public void Info(string message, params object[] args)
        {
            _log.Info(string.Format(message, args));
        }

        public void Info(string message)
        {
            _log.Info(message);
        }
        
        public void Warning(string message, params object[] args)
        {
            _log.Warn(string.Format(message, args));
        }

        public void Warning(string message)
        {
            _log.Warn(message);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Error(Exception ex)
        {
            _log.Error("Application error.", ex);
        }
        
        public void Fatal(string message, Exception ex)
        {
            _log.Fatal(message, ex);
        }

        public void Fatal(Exception ex)
        {
            _log.Fatal("Application error.", ex);
        }
    }
}