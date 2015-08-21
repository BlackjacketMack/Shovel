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

namespace Shovel.Command
{
    public class Communicator : ICommunicator
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
