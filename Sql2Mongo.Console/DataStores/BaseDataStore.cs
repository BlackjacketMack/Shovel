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
using System.Configuration;

namespace Sql2Mongo.Command
{
    /// <summary>
    /// Can't be abstract because we deserialize to this type.
    /// </summary>
    internal abstract class BaseDataStore
    {
        public DataStoreTypes DataStoreType { get; set; }
        public string ConnectionStringName { get; set; }
        public int BatchSize { get; set; }

        public string ExportQuery { get; set; }
        public string ImportQuery { get; set; }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;
        }
    }
}
