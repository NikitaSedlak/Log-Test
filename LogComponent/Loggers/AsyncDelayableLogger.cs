using LogComponent.Models;
using LogComponent.Services;
using System.Text;

namespace LogComponent.Loggers
{
    public sealed class AsyncDelayableLogger : LoggerBase, IDelayableLogger
    {
        public AsyncDelayableLogger(IDateTimeService dateTimeService, IStreamWriterService streamWriterService, int delay = 0) : base(dateTimeService, streamWriterService)
        {
            Delay = delay;
        }

        public int Delay { get; private set; }

        public override async Task WriteAsync(IAsyncEnumerable<LogLine> lineStream)
        {
            try
            {
                var writer = GetStreamWriter();

                await foreach (var line in lineStream)
                {
                    if (IsStopped)
                    {
                        Console.WriteLine("Operation was stopped");
                        return;
                    }

                    StringBuilder stringBuilder = new();

                    if (ShouldRepublishStreamWriter())
                    {
                        writer = GetStreamWriter();
                    }

                    stringBuilder.Append(line.Timestamp.ToString(TimeStampFormat));
                    stringBuilder.Append('\t');
                    stringBuilder.Append(line.LineText());
                    stringBuilder.Append('\t');
                    stringBuilder.Append(Environment.NewLine);

                    await writer.WriteAsync(stringBuilder.ToString()).ConfigureAwait(false);

                    await Task.Delay(Delay).ConfigureAwait(false);
                }

                writer.Dispose();
                Console.WriteLine("Operation is done");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception", ex);
                throw;
            }
        }
    }
}