using LogComponent.Models;

namespace LogComponent.Loggers
{
    public interface ILog
    {
        string Name { get; }

        public bool IsStopped { get; }

        /// <summary>
        /// Stop the logging. If any outstadning logs theses will not be written to Log
        /// </summary>
        void Stop();

        /// <summary>
        /// Write a message to the Log.
        /// </summary>
        /// <param name="lineStream">Represens a stream of log messages</param>
        Task WriteAsync(IAsyncEnumerable<LogLine> lineStream);
    }
}
