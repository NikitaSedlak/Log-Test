using LogComponent.Loggers;
using LogComponent.Models;

namespace LogComponent.LoggerRunners
{
    public class Runner : IRunner
    {
        public Func<int, bool> Condition { get; private set; }
        public Func<int, int> Increment { get; private set; }
        public string? Msg { get; private set; }
        public int? StartPoint { get; private set; }

        public void RunNewLogger(ILog logger)
        {
            _ = Task.Run(() => logger.WriteAsync(GetLogLines()));
        }

        public void SetCondition(Func<int, bool> condition) => Condition = condition;

        public void SetIncrement(Func<int, int> increment) => Increment = increment;

        public void SetMsg(string msg) => Msg = msg;

        public void SetStartPoint(int startPoint) => StartPoint = startPoint;

        protected virtual async IAsyncEnumerable<LogLine> GetLogLines()
        {
            for (int i = StartPoint!.Value; Condition(i); i = Increment(i))
            {
                yield return new LogLine(DateTime.Now, Msg + i.ToString());
            }
        }
    }
}