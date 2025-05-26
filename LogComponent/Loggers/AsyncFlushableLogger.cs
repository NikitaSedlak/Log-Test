using LogComponent.Models;
using System.Text;

namespace LogComponent.Loggers
{
    public sealed class AsyncFlushableLogger : LoggerBase, IFlushableLogger
    {
        public AsyncFlushableLogger(DateTime initDate) : base(initDate) { }

        public bool IsStoppedWithFlush { get; private set; } = false;

        public override async Task WriteAsync(IAsyncEnumerable<LogLine> lineStream)
        {
            try
            {
                var writer = GetStreamWriter();

                await foreach (var line in lineStream)
                {
                    if (IsStopped && !IsStoppedWithFlush)
                    {
                        Console.WriteLine("Operation was stopped");
                        return;
                    }

                    StringBuilder stringBuilder = new();

                    if (!IsStreamWriterStillValid())
                    {
                        writer = GetStreamWriter();
                    }

                    if (IsStoppedWithFlush)
                    {
                        stringBuilder.Append("Flush stop: ");
                    }

                    stringBuilder.Append(line.Timestamp.ToString(TimeStampFormat));
                    stringBuilder.Append('\t');
                    stringBuilder.Append(line.LineText());
                    stringBuilder.Append('\t');
                    stringBuilder.Append(Environment.NewLine);

                    await writer.WriteAsync(stringBuilder.ToString()).ConfigureAwait(false);
                }


                Console.WriteLine("Operation is done");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception", ex);
                throw;
            }
        }

        public void StopWithFlush() => IsStoppedWithFlush = true;
    }
}
