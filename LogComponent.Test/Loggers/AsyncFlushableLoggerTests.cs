using FluentAssertions;
using LogComponent.Loggers;
using LogComponent.Models;
using LogComponent.Services;
using Moq;

namespace LogComponent.Test.Loggers
{
    [TestFixture]
    public class AsyncFlushableLoggerTests : LoggerTestsBase
    {
        [TestCaseSource(nameof(GetTestLogLines))]
        public async Task WriteAsync_NotStopped_ShouldSaveAllItems(int itemsCount, IAsyncEnumerable<LogLine> lines)
        {
            // Given
            GivenDateTimeServiceReturnsMiddleDayDateTime();
            GivenStreamWriter();

            // When
            var act = async () => await Sut().WriteAsync(lines);

            // Then
            await act.Should().NotThrowAsync();
            
            ThenStreamWriterCreationShouldBeCalledOnce();
            ThenWriterWriteAsyncShouldBeCalled(itemsCount);
        }

        [TestCaseSource(nameof(GetTestLogLines))]
        public async Task WriteAsync_Stopped_ShouldSaveFewItems(int itemsCount, IAsyncEnumerable<LogLine> lines)
        {
            // Given
            GivenDateTimeServiceReturnsMiddleDayDateTime();
            GivenStreamWriter();
            var sut = Sut();

            // When
            var act = async () => await sut.WriteAsync(lines);
            sut.Stop();

            // Then
            await act.Should().NotThrowAsync();

            ThenStreamWriterCreationShouldBeCalledOnce();
            ThenWriterWriteAsyncShouldBeCalledBetween(itemsCount);
        }

        [TestCaseSource(nameof(GetTestLogLines))]
        public async Task WriteAsync_StoppedWithFlush_ShouldSaveAllItems(int itemsCount, IAsyncEnumerable<LogLine> lines)
        {
            // Given
            GivenDateTimeServiceReturnsMiddleDayDateTime();
            GivenStreamWriter();
            var sut = Sut();

            // When
            var act = async () => await sut.WriteAsync(lines);
            sut.StopWithFlush();

            // Then
            await act.Should().NotThrowAsync();

            ThenStreamWriterCreationShouldBeCalledOnce();
            ThenWriterWriteAsyncShouldBeCalled(itemsCount);
        }

        [TestCaseSource(nameof(GetTestLogLines))]
        public async Task WriteAsync_CrossMidnight_ShouldSaveAllItems(int itemsCount, IAsyncEnumerable<LogLine> lines)
        {
            // Given
            GivenDateTimeServiceReturnsTwoDifferentDateTime();
            GivenStreamWriter();
            var sut = Sut();

            // When
            var act = async () => await sut.WriteAsync(lines);

            // Then
            await act.Should().NotThrowAsync();

            ThenStreamWriterCreationShouldBeCalledTwice();
            ThenWriterWriteAsyncShouldBeCalled(itemsCount);
        }

        [Test]
        public async Task WriteAsync_NotStopped_ShouldThrow()
        {
            // Given
            _streamWriterServiceMock
                .Setup(x => x.GetStreamWriter(It.IsAny<DateTime>(), It.IsAny<string>())).Throws(new Exception());

            // When
            var act = async () => await Sut().WriteAsync(It.IsAny<IAsyncEnumerable<LogLine>>());

            // Then
            await act.Should().ThrowAsync<Exception>();
        }

        private AsyncFlushableLogger Sut()
        {
            return new AsyncFlushableLogger(_dateTimeServiceMock.Object, _streamWriterServiceMock.Object);
        }
    }
}
