using LogComponent.Loggers;
using LogComponent.Models;

namespace LogComponent.LoggerRunners
{
    public class DelayableRunner : IDelayableRunner, IRunner
    {
        private readonly Dictionary<string, CancellationTokenSource> _activeLoggers = new();

        public int? StartPoint { get; private set; }
        public Func<int, bool> Condition { get; private set; }
        public Func<int, int> Increment { get; private set; }
        public int? Delay { get; private set; }
        public string? Msg { get; private set; }

        public void StartNewLogger(ILog logger)
        {
            var cts = new CancellationTokenSource();
            _activeLoggers[logger.Name] = cts;

            _ = Task.Run(() => logger.WriteAsync(GetLogLines(cts.Token), cts.Token));
        }

        public void SetStartPoint(int startPoint) => StartPoint = startPoint;

        public void SetCondition(Func<int, bool> condition) => Condition = condition;

        public void SetIncrement(Func<int, int> increment) => Increment = increment;

        public void SetDelay(int delay) => Delay = delay;

        public void SetMsg(string msg) => Msg = msg;

        private async IAsyncEnumerable<LogLine> GetLogLines(CancellationToken token)
        {
            for (int i = StartPoint!.Value; Condition(i); i = Increment(i))
            {
                if (token.IsCancellationRequested)
                    yield break;

                await Task.Delay(Delay!.Value);
                yield return new LogLine(DateTime.Now, Msg + i.ToString());
            }
        }
    }
}