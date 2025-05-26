using LogComponent.Loggers;
using LogComponent.Models;

namespace LogComponent.LoggerRunners
{
    public class DelayableRunner : Runner, IDelayableRunner
    {
        public int? Delay { get; private set; }

        public void SetDelay(int delay) => Delay = delay;

        protected override async IAsyncEnumerable<LogLine> GetLogLines()
        {
            for (int i = StartPoint!.Value; Condition(i); i = Increment(i))
            {
                await Task.Delay(Delay!.Value);
                yield return new LogLine(DateTime.Now, Msg + i.ToString());
            }
        }
    }
}