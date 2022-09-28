using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTraderApplicationProject.Application.Logging;

namespace TeleTraderApplicationProject.Implementation.Logging
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public void Log(Exception ex)
        {
            Console.WriteLine($"Occured At:{DateTime.UtcNow}\nMessage:{ex.Message}\nInner exception: {ex.InnerException}\nStack trace: {ex.StackTrace}");
        }
    }
}
