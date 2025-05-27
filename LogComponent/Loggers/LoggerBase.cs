using LogComponent.Models;
using LogComponent.Services;

namespace LogComponent.Loggers
{
    public abstract class LoggerBase : ILog
    {
        private readonly DateTime _initDate;
        private readonly IDateTimeService _dateTimeService;
        private readonly IStreamWriterService _streamWriterService;

        private IStreamLogWriter? _writer;
        private DateTime _currDate;
        private DateTime? _republishDate;

        protected LoggerBase(IDateTimeService dateTimeService, IStreamWriterService streamWriterService)
        {
            if (!Directory.Exists(@"C:\LogTest"))
                Directory.CreateDirectory(@"C:\LogTest");

            _initDate = dateTimeService.GetCurrentDateTime();
            _currDate = dateTimeService.GetCurrentDateTime();
            _republishDate = null;
            _dateTimeService = dateTimeService;
            _streamWriterService = streamWriterService;
        }

        protected virtual string TimeStampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss:fff";

        public string Name { get; private set; } = Guid.NewGuid().ToString();

        public bool IsStopped { get; private set; } = false;

        protected virtual IStreamLogWriter GetStreamWriter()
        {
            if (_writer is null || ShouldRepublishStreamWriter())
            {
                _currDate = _dateTimeService.GetCurrentDateTime();
                _republishDate = _writer is not null ? _dateTimeService.GetCurrentDateTime() : null;

                _writer?.Dispose();

                _writer = _streamWriterService.GetStreamWriter(_currDate, Name);
            }

            return _writer;
        }

        protected virtual bool ShouldRepublishStreamWriter()
        {
            return _dateTimeService.GetCurrentDateTime().Date > (_republishDate ?? _initDate).Date;
        }

        public abstract Task WriteAsync(IAsyncEnumerable<LogLine> lineStream);

        public virtual void Stop() => IsStopped = true;
    }
}
