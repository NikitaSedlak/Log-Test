using LogComponent.Models;
using System.Text;

namespace LogComponent.Loggers
{
    public sealed class AsyncDelayableLogger : LoggerBase, IDelayableLogger
    {
        public AsyncDelayableLogger(int delay = 0) : base()
        {
            Delay = delay;
        }

        public int Delay { get; private set; }

        public override void StopWithFlush()
        {
            throw new NotImplementedException();
        }

        public override void StopWithoutFlush()
        {
            throw new NotImplementedException();
        }

        public override async Task WriteAsync(IAsyncEnumerable<LogLine> lineStream, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                var writer = GetStreamWriter();

                await foreach (var line in lineStream)
                {
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }

                    StringBuilder stringBuilder = new();

                    if (!IsStreamWriterStillValid())
                    {
                        writer = GetStreamWriter();
                    }

                    stringBuilder.Append(line.Timestamp.ToString(TimeStampFormat));
                    stringBuilder.Append('\t');
                    stringBuilder.Append(line.LineText());
                    stringBuilder.Append('\t');
                    stringBuilder.Append(Environment.NewLine);

                    await writer.WriteAsync(stringBuilder.ToString()).ConfigureAwait(false);

                    await Task.Delay(Delay, ct).ConfigureAwait(false);
                }

                Console.WriteLine("Operation is done");

                return;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was canceled");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception", ex);
                throw;
            }
        }
    }
}