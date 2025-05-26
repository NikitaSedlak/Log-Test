using LogComponent.Loggers;

namespace LogComponent.LoggerRunners
{
    public interface IRunner
    {
        public Func<int, bool> Condition { get; }
        public Func<int, int> Increment { get; }
        public string? Msg { get; }
        public int? StartPoint { get; }

        void RunNewLogger(ILog logger);

        void SetCondition(Func<int, bool> condition);

        void SetIncrement(Func<int, int> increment);

        void SetMsg(string msg);

        void SetStartPoint(int startPoint);
    }
}