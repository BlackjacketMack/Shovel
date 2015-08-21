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
    public interface ICommunicator
    {
        void Write(string message);
        void WriteLine(string message);
    }
}
