using LogComponent.Models;

namespace LogComponent.Loggers
{
    public interface ILog
    {
        string Name { get; }

        /// <summary>
        /// Stop the logging. If any outstadning logs theses will not be written to Log
        /// </summary>
        void StopWithoutFlush();

        /// <summary>
        /// Stop the logging. The call will not return until all all logs have been written to Log.
        /// </summary>
        void StopWithFlush();

        /// <summary>
        /// Write a message to the Log.
        /// </summary>
        /// <param name="lineStream">Represens a stream of log messages</param>
        Task WriteAsync(IAsyncEnumerable<LogLine> lineStream, CancellationToken ct);
    }
}
