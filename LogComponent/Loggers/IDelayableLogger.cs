using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent.Loggers
{
    public interface IDelayableLogger
    {
        public int Delay { get; }
    }
}
