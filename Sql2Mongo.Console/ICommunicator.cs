﻿using System;
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
    /// <summary>
    /// Used to transmit messages back to calling processes.
    /// Mimicks Console.WriteLine()...etc. 
    /// </summary>
    internal interface ICommunicator
    {
        void Write(string message);
        void WriteLine(string message);
    }
}
