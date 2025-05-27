using LogComponent.Models;
using LogComponent.Services;
using Moq;

namespace LogComponent.Test.Loggers
{
    [TestFixture]
    public class LoggerTestsBase
    {
        [SetUp]
        public void Setup()
        {
            _dateTimeServiceMock = new Mock<IDateTimeService>();
            _streamWriterServiceMock = new Mock<IStreamWriterService>();
            _writerMock = new Mock<IStreamLogWriter>();
        }

        protected Mock<IDateTimeService> _dateTimeServiceMock;
        protected Mock<IStreamWriterService> _streamWriterServiceMock;
        protected Mock<IStreamLogWriter> _writerMock;

        protected static async IAsyncEnumerable<LogLine> GetAsyncLogLines(IEnumerable<LogLine> items)
        {
            foreach (var item in items)
            {
                yield return item;
                await Task.Yield();
            }
        }

        protected static IEnumerable<object[]> GetTestLogLines()
        {
            yield return new object[] { 10, GetAsyncLogLines(new[] {
                new LogLine(DateTime.Now, "line 1"),
                new LogLine(DateTime.Now, "line 2"),
                new LogLine(DateTime.Now, "line 3"),
                new LogLine(DateTime.Now, "line 4"),
                new LogLine(DateTime.Now, "line 5"),
                new LogLine(DateTime.Now, "line 6"),
                new LogLine(DateTime.Now, "line 7"),
                new LogLine(DateTime.Now, "line 8"),
                new LogLine(DateTime.Now, "line 9"),
                new LogLine(DateTime.Now, "line 10"),
            }) };
        }

        protected void GivenDateTimeServiceReturnsMiddleDayDateTime()
        {
            _dateTimeServiceMock
                .Setup(x => x.GetCurrentDateTime())
                .Returns(DateTime.Now);
        }

        protected void GivenDateTimeServiceReturnsTwoDifferentDateTime()
        {
            _dateTimeServiceMock
                .SetupSequence(x => x.GetCurrentDateTime())
                .Returns(new DateTime(2025, 5, 27, 23, 59, 59, 998))
                .Returns(new DateTime(2025, 5, 27, 23, 59, 59, 998))
                .Returns(new DateTime(2025, 5, 27, 23, 59, 59, 998))
                .Returns(new DateTime(2025, 5, 28, 00, 00, 01, 2))
                .Returns(new DateTime(2025, 5, 28, 00, 00, 01, 2))
                .Returns(new DateTime(2025, 5, 28, 00, 00, 01, 2));
        }

        protected void GivenStreamWriter()
        {
            _streamWriterServiceMock
                .Setup(x => x.GetStreamWriter(It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(_writerMock.Object);
        }

        protected void ThenStreamWriterCreationShouldBeCalledOnce()
        {
            _streamWriterServiceMock
                .Verify(x => x.GetStreamWriter(It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
        }

        protected void ThenStreamWriterCreationShouldBeCalledTwice()
        {
            _streamWriterServiceMock
                .Verify(x => x.GetStreamWriter(It.IsAny<DateTime>(), It.IsAny<string>()), Times.Exactly(2));
        }

        protected void ThenWriterWriteAsyncShouldBeCalled(int times)
        {
            _writerMock
                .Verify(x => x.WriteAsync(It.IsAny<string>()), Times.Exactly(times));
        }

        protected void ThenWriterWriteAsyncShouldBeCalledBetween(int maxtimes)
        {
            _writerMock
                .Verify(x => x.WriteAsync(It.IsAny<string>()), Times.Between(0, maxtimes, Moq.Range.Inclusive));
        }
    }
}