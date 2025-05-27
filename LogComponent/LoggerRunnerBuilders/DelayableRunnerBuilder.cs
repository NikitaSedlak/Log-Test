using LogComponent.LoggerRunners;
using LogComponent.Loggers;

namespace LogComponent.LoggerRunnerBuilders
{
    public class DelayableRunnerBuilder : RunnerBuilder<DelayableRunner>
    {
        public DelayableRunnerBuilder() : base()
        {
        }

        public RunnerBuilder<DelayableRunner> SetDelay(int delay)
        {
            _runner.SetDelay(delay);

            return this;
        }

        protected override bool IsInvalid()
        {
            return _runner.Delay is null || base.IsInvalid();
        }
    }
}
