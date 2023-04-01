using NLog;
using NLog.Web;

namespace Dealty.WebApi.Logger
{
    public interface IDealtyLogger
    {
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
        void Error(Exception exp);
    }

    public class DealtyLogger : IDealtyLogger
    {
        //private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        private static readonly NLog.ILogger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        public DealtyLogger()
        {
        }


        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception exp)
        {
            logger.Error(exp);
        }
    }
}
