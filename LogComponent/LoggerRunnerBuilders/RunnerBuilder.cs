using LogComponent.LoggerRunners;

namespace LogComponent.LoggerRunnerBuilders
{
    public class RunnerBuilder<T> : IRunnerBuilder where T : IRunner, new()
    {
        protected T _runner;

        public RunnerBuilder()
        {
            _runner = new();
        }

        public IRunner Build()
        {
            if (IsInvalid())
            {
                throw new InvalidOperationException("");
            }

            var validResult = _runner;

            Reset();

            return validResult;
        }

        public void Reset()
        {
            _runner = new();
        }

        protected virtual bool IsInvalid()
        {
            return _runner.Increment is null
                || _runner.Condition is null
                || _runner.Msg is null
                || _runner.StartPoint is null;
        }

        public RunnerBuilder<T> SetCondition(Func<int, bool> condition)
        {
            _runner.SetCondition(condition);

            return this;
        }

        public RunnerBuilder<T> SetIncrement(Func<int, int> increment)
        {
            _runner.SetIncrement(increment);

            return this;
        }

        public RunnerBuilder<T> SetMsg(string msg)
        {
            _runner.SetMsg(msg);

            return this;
        }

        public RunnerBuilder<T> SetStartPoint(int startPoint)
        {
            _runner.SetStartPoint(startPoint);

            return this;
        }
    }

    public sealed class RunnerBuilder : RunnerBuilder<Runner>
    {
        public RunnerBuilder() 
            : base()
        {
            
        }
    }
}