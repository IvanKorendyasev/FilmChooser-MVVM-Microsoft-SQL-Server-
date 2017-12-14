using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Diagnostics;

namespace NLogProject
{
    public class LogMaker
    {
        private Logger _logger;

        public LogMaker()
        {
            _logger = LogManager.GetLogger("NLog");
        }

        public void LogTrace(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Trace($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
        public void LogDebug(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Debug($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
        public void LogInfo(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Info($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
        public void LogWarn(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Warn($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
        public void LogError(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Error($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
        public void LogFatal(string message)
        {
            var sf = new StackFrame(1);
            var method = sf.GetMethod();
            _logger.Fatal($"{method.DeclaringType.FullName}.{method.Name}: {message}");
        }
    }
}
