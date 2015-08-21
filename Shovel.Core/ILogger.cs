using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Shovel.Core
{
    /// <summary>
    /// Used to transmit messages back to calling processes.
    /// Mimicks Console.WriteLine()...etc. 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes to an implemented console (for example).
        /// </summary>
        /// <param name="message"></param>
        void Write(string message);
        void WriteLine(string message);

        /// <summary>
        /// Logs to an event log (text file for example)
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);
    }
}
