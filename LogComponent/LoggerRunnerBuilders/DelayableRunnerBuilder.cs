using LogComponent.LoggerRunners;
using LogComponent.Loggers;

namespace LogComponent.LoggerRunnerBuilders
{
    public class DelayableRunnerBuilder : IRunnerBuilder
    {
        private DelayableRunner _delayableRunner;


        public DelayableRunnerBuilder()
        {
            _delayableRunner = new DelayableRunner();
        }

        public DelayableRunnerBuilder SetStartPoint(int startPoint)
        {
            _delayableRunner.SetStartPoint(startPoint);

            return this;
        }

        public DelayableRunnerBuilder SetCondition(Func<int, bool> condition)
        {
            _delayableRunner.SetCondition(condition);

            return this;
        }

        public DelayableRunnerBuilder SetIncrement(Func<int, int> increment)
        {
            _delayableRunner.SetIncrement(increment);

            return this;
        }

        public DelayableRunnerBuilder SetDelay(int delay)
        {
            _delayableRunner.SetDelay(delay);

            return this;
        }

        public DelayableRunnerBuilder SetMsg(string msg)
        {
            _delayableRunner.SetMsg(msg);

            return this;
        }

        public IRunner Build()
        {
            if (_delayableRunner.Delay is null
                || _delayableRunner.Increment is null
                || _delayableRunner.Condition is null
                || _delayableRunner.Msg is null
                || _delayableRunner.StartPoint is null)
            {
                throw new InvalidOperationException("");
            }

            var validResult = _delayableRunner;

            Reset();

            return validResult;
        }

        public void Reset()
        {
            _delayableRunner = new();
        }
    }
}
