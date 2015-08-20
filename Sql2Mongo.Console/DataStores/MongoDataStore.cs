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
    internal class MongoDataStore : BaseDataStore, IDataStore
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public MongoDataStore()
        {
        }

        public IEnumerable<T> Get<T>()
        {
            throw new NotImplementedException();
        }

        public async void Insert(object obj)
        {
            var connectionString = getConnectionString();

            var mongoClient = new MongoClient();
            var db = mongoClient.GetDatabase(this.DatabaseName);
            var collection = db.GetCollection<object>(this.CollectionName);

            await collection.InsertOneAsync(obj);
        }

        private string getConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;

            return connectionString;
        }
    }
}
