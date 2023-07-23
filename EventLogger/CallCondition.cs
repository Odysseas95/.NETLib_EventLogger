using Email_Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Email_Parser.Logger;

namespace EventLogger
{
    internal class CallCondition
    {
        public static Logger Logger;

        public CallCondition()
        {
            Logger = new Logger();

            try
            {
                Logger.LogEvent($"Im a log string for an Error", LoggerType.Error);
                Logger.LogEvent($"Im a log string for a Warning", LoggerType.Warning);
                Logger.LogEvent($"Im a log string for a Succeful operation", LoggerType.Success);
                Logger.LogEvent($"Im a log string for a Workaround taken place", LoggerType.Workaround);

            }
            catch (Exception ex) 
            {
                Logger.LogEvent(ex.Message, LoggerType.Error);
            }
            
        }
    }
}
