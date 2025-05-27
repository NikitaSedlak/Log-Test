using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
