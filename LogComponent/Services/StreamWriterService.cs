using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogComponent.Services
{
    public class StreamWriterService : IStreamWriterService
    {
        public IStreamLogWriter GetStreamWriter(DateTime dateTime, string name)
        {
            var writer = File.AppendText(@"C:\LogTest\Log" + dateTime.ToString("yyyyMMdd HHmmss fff") + name + ".log");
            writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
            writer.AutoFlush = true;

            return new StreamLogWriter(writer);
        }
    }
}
