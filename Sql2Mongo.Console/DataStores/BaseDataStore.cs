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

namespace Sql2Mongo.Command
{
    /// <summary>
    /// Can't be abstract because we deserialize to this type.
    /// </summary>
    internal class BaseDataStore
    {
        public DataStoreTypes DataStoreType { get; set; }
        public string ConnectionStringName { get; set; }
    }
}
