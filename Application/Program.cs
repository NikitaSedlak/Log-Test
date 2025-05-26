using LogComponent.LoggerRunnerBuilders;
using LogComponent.LoggerRunners;
using LogComponent.Loggers;

namespace LogUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            //ILog  logger = new AsyncLog();

            //for (int i = 0; i < 15; i++)
            //{
            //    logger.Write("Number with Flush: " + i.ToString());
            //    Thread.Sleep(50);
            //}

            //logger.StopWithFlush();

            //ILog logger2 = new AsyncLog();

            //for (int i = 50; i > 0; i--)
            //{
            //    logger2.Write("Number with No flush: " + i.ToString());
            //    Thread.Sleep(20);
            //}

            //logger2.StopWithoutFlush();

            DelayableRunnerBuilder builder = new DelayableRunnerBuilder();

            ILog logger1 = new AsyncDelayableLogger(50);

            IRunner runner1 = builder.SetStartPoint(0)
                .SetCondition(i => i < 15)
                .SetIncrement(i => i + 1)
                .SetDelay(50)
                .SetMsg("Number with Flush: ")
                .Build();

            runner1.StartNewLogger(logger1);

            //

            ILog logger2 = new AsyncDelayableLogger(50);

            IRunner runner2 = builder.SetStartPoint(50)
                .SetCondition(i => i > 0)
                .SetIncrement(i => i - 1)
                .SetDelay(20)
                .SetMsg("Number with No flush: ")
                .Build();

            runner2.StartNewLogger(logger2);

            Console.ReadLine();
        }
    }
}
