using LogComponent.LoggerRunners;
using LogComponent.Loggers;

namespace LogComponent.LoggerRunnerBuilders
{
    public class DelayableRunnerBuilder : RunnerBuilder<DelayableRunner>
    {
        public DelayableRunnerBuilder(int delay) : base()
        {
            _runner.SetDelay(delay);
        }

        protected override bool IsInvalid()
        {
            return _runner.Delay is null || base.IsInvalid();
        }
    }
}
