namespace LogComponent.LoggerRunners
{
    public interface IDelayableRunner
    {
        int? Delay { get; }

        void SetDelay(int delay);
    }
}