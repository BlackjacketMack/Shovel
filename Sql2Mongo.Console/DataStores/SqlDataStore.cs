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
    internal class SqlDataStore : BaseDataStore, IDataStore
    {
        public IEnumerable<T> Get<T>()
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
