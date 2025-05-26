using LogComponent.Loggers;

namespace LogComponent.LoggerRunners
{
    public interface IRunner
    {
        void StartNewLogger(ILog logger);
    }
}