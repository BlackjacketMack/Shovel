using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using Newtonsoft.Json;
using Shovel.Core;
using System.IO;

namespace Shovel.Command
{
    public class Logger : ILogger
    {
        private string _logFile;

        public Logger(string logFile)
        {
            _logFile = logFile;
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message)
        {
            using (StreamWriter w = File.AppendText(_logFile))
            {
                w.Write("\r" + message, w);
            }
        }
    }
}
