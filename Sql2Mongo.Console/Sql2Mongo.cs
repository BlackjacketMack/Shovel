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
    internal class Sql2Mongo
    {
        private Definition _definition;

        public void Process(Definition definition)
        {
            _definition = definition;

            var def = "{User:{UserID:@UserID,UserName:\"@UserName\"}}";

            var items = new[] { new { UserID = 4, UserName = "Rob" } };

            var properties = !items.Any() ? Enumerable.Empty<PropertyInfo>() : items.First().GetType().GetProperties().Cast<PropertyInfo>();

            var itemsConvertedToJson = items.Select(s => getJsonDoc(s, def, properties));

            var task = insertMongo(itemsConvertedToJson);

            task.Wait();
        }

        private static string getJsonDoc(object obj, string definition, IEnumerable<PropertyInfo> properties)
        {
            var json = definition;

            foreach (var prop in properties)
            {
                var val = prop.GetValue(obj);
                json = json.Replace("@" + prop.Name, val.ToString());
            }

            if (json.Equals(definition))
            {
                throw new ApplicationException("No transformation occurred.");
            }

            return json;
        }

        private static async Task<IEnumerable<Task>> insertMongo(IEnumerable<string> json)
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("testdb");
            var collection = db.GetCollection<BsonDocument>("Sql2MongoStuff");

            var tasks = new List<Task>();
            
            foreach (var jsonFile in json)
            {
                var bsonDoc = BsonDocument.Parse(jsonFile);
                await collection.InsertOneAsync(bsonDoc);
            }

            return tasks;
        }
       
    }
}
