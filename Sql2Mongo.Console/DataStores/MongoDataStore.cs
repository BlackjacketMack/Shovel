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
using Shovel.Core;

namespace Shovel.Command
{
    internal class MongoDataStore : BaseDataStore, IDataStore
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        private static readonly MongoClient _mongoClient = new MongoClient();

        public MongoDataStore()
        {
        }


        public long ExportCountTotal()
        {
            throw new NotImplementedException();
        }

        public void Import(IEnumerable<object> objs)
        {
            var connectionString = this.GetConnectionString();

            var db = _mongoClient.GetDatabase(this.DatabaseName);
            var collection = db.GetCollection<BsonDocument>(this.CollectionName);

            var bsons = new List<BsonDocument>();
            foreach (var obj in objs)
            {
                var objSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var bson = BsonDocument.Parse(objSerialized);

                bsons.Add(bson);
            }

            var task = collection.InsertManyAsync(bsons);
            //task.Wait();
        }


        public IEnumerable<object> Export(long startRow, long maxRows)
        {
            throw new NotImplementedException();
        }
    }
}
