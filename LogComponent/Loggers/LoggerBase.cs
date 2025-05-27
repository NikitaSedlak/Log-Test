using LogComponent.Models;
using LogComponent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent.Loggers
{
    public abstract class LoggerBase : ILog
    {
        private readonly DateTime _initDate;
        private readonly IDateTimeService _dateTimeService;

        private StreamWriter? _writer;

        protected LoggerBase(IDateTimeService dateTimeService)
        {
            if (!Directory.Exists(@"C:\LogTest"))
                Directory.CreateDirectory(@"C:\LogTest");

            _initDate = dateTimeService.GetCurrentDateTime();
            _dateTimeService = dateTimeService;
        }

        protected virtual string TimeStampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss:fff";

        public string Name { get; private set; } = Guid.NewGuid().ToString();

        public bool IsStopped { get; private set; } = false;

        protected virtual StreamWriter GetStreamWriter()
        {
            if (_writer is null || !IsStreamWriterStillValid())
            {
                _writer?.Dispose();

                var writer = File.AppendText(@"C:\LogTest\Log" + _dateTimeService.GetCurrentDateTime().ToString("yyyyMMdd HHmmss fff") + Name + ".log");
                writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
                writer.AutoFlush = true;

                _writer = writer;
            }

            return _writer;
        }

        protected virtual bool IsStreamWriterStillValid() => (_dateTimeService.GetCurrentDateTime() - _initDate).Days == 0;

        public abstract Task WriteAsync(IAsyncEnumerable<LogLine> lineStream);

        public virtual void Stop() => IsStopped = true;
    }
}
