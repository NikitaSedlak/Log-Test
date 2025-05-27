
namespace LogComponent.Services
{
    // For test purposes
    public class StreamLogWriter : IStreamLogWriter
    {
        private readonly StreamWriter _streamWriter;

        public StreamLogWriter(StreamWriter writer)
        {
            _streamWriter = writer;
        }

        public Task WriteAsync(string text) =>
            _streamWriter.WriteAsync(text);

        public void Dispose()
        {
            _streamWriter.Dispose();
        }
    }
}