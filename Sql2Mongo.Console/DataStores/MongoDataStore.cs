using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Configuration;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace Sql2Mongo.Command
{
    internal class MongoDataStore : BaseDataStore, IDataStore
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public MongoDataStore()
        {
        }

        public IEnumerable<object> Export()
        {
            throw new NotImplementedException();
        }

        public long ExportCountTotal()
        {
            throw new NotImplementedException();
        }

        public async void Import(object obj)
        {
            var connectionString = this.GetConnectionString();

            var mongoClient = new MongoClient();
            var db = mongoClient.GetDatabase(this.DatabaseName);
            var collection = db.GetCollection<BsonDocument>(this.CollectionName);

            var objSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var bson = BsonDocument.Parse(objSerialized);
            await collection.InsertOneAsync(bson);
        }


        
    }
}
