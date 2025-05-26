using LogComponent.LoggerRunners;

namespace LogComponent.LoggerRunnerBuilders
{
    public interface IRunnerBuilder
    {
        IRunner Build();

        void Reset();
    }
}