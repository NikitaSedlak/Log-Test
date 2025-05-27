using FluentAssertions;
using LogComponent.LoggerRunnerBuilders;
using LogComponent.LoggerRunners;
using LogComponent.Loggers;
using LogComponent.Services;
using Moq;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogComponent.Test.Builders
{
    [TestFixture]
    public class DelayableRunnerBuilderTests
    {
        private Mock<IDateTimeService> _dateTimeServiceMock;

        [SetUp]
        public void Setup()
        {
            _dateTimeServiceMock = new Mock<IDateTimeService>();
        }

        [TestCaseSource(nameof(TestData))]
        public async Task AsyncDelayableLogger_RunAndCrossMidnight_ShouldCreateTwoLogFiles(DateTime firstDay, DateTime secondDay)
        {
            // Arrange
            _dateTimeServiceMock
                .SetupSequence(x => x.GetCurrentDateTime())
                .Returns(firstDay)
                .Returns(firstDay)
                .Returns(firstDay)
                .Returns(secondDay)
                .Returns(secondDay)
                .Returns(secondDay);

            ILog logger = new AsyncDelayableLogger(_dateTimeServiceMock.Object, new StreamWriterService(), 10);

            IRunner runner = new DelayableRunnerBuilder()
                .SetDelay(10)
                .SetStartPoint(0)
                .SetCondition(i => i < 10)
                .SetIncrement(i => i + 1)
                .SetMsg("Number with No flush: ")
                .Build();

            string firstFileExpectedName = @"C:\LogTest\Log" + firstDay.ToString("yyyyMMdd HHmmss fff") + logger.Name + ".log";
            string secondFileExpectedName = @"C:\LogTest\Log" + secondDay.ToString("yyyyMMdd HHmmss fff") + logger.Name + ".log";

            // Act
            runner.RunNewLogger(logger);
            
            await Task.Delay(10000);

            // Assert
            File.Exists(firstFileExpectedName).Should().BeTrue();
            File.Exists(secondFileExpectedName).Should().BeTrue();

            // Cleanup
            File.Delete(firstFileExpectedName);
            File.Delete(secondFileExpectedName);
        }

        private static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new DateTime(2025, 5, 27, 23, 59, 59, 950), new DateTime(2025, 5, 28, 00, 00, 50, 2) };
        }
    }
}
