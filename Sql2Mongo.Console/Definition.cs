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

namespace Sql2Mongo.Command
{
    internal class Definition
    {
        public string Name { get; set; }
        public int BatchSize { get; set; }

        public IDataStore SourceDataStore { get; set; }

        public IDataStore DestinationDataStore { get; set; }

        public Definition()
        {

        }
    }
}
