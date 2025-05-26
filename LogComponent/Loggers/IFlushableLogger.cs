namespace LogComponent.Loggers
{
    public interface IFlushableLogger
    {
        bool IsStoppedWithFlush { get; }

        /// <summary>
        /// Stop the logging. The call will not return until all all logs have been written to Log.
        /// </summary>
        void StopWithFlush();
    }
}