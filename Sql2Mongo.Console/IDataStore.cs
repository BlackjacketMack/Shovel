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
    internal interface IDataStore
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get<T>();
        void Insert<T>(T obj);
    }
}
