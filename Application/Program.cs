using LogComponent.LoggerRunnerBuilders;
using LogComponent.LoggerRunners;
using LogComponent.Loggers;
using LogComponent.Services;

namespace LogUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger1 = new AsyncDelayableLogger(new DateTimeService(), 50);

            IRunner runner1 = new DelayableRunnerBuilder().SetDelay(50)
                .SetStartPoint(0)
                .SetCondition(i => i < 15)
                .SetIncrement(i => i + 1)
                .SetMsg("Number with No flush: ")
                .Build();

            runner1.RunNewLogger(logger1);
            //logger1.Stop();

            // --- *** ---

            AsyncFlushableLogger logger2 = new AsyncFlushableLogger(new DateTimeService());

            IRunner runner2 = new RunnerBuilder().SetStartPoint(50)
                .SetCondition(i => i > 0)
                .SetIncrement(i => i - 1)
                .SetMsg("Number with flush: ")
                .Build();

            runner2.RunNewLogger(logger2);

            //Thread.Sleep(15);

            //logger2.StopWithFlush();
            //logger2.Stop();

            Console.ReadLine();
        }
    }
}
