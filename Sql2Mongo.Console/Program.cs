using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.IO;


using System.Configuration;
using Newtonsoft.Json;
namespace Sql2Mongo.Command
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sql2Mongo = new Sql2Mongo();


            var definitionsJson = File.ReadAllText(ConfigurationManager.AppSettings["DefinitionFile"]);

            var definitions = deserializeDefinitions(definitionsJson);

            //sql2Mongo.Process(null);


            Console.Write("ok");
            Console.ReadLine();
        }


        private static IEnumerable<Definition> deserializeDefinitions(string definitionsJson)
        {
            var definitions = JsonConvert.DeserializeObject<IEnumerable<Definition>>(definitionsJson, new JsonSerializerSettings
            {
                Converters = new[] { new DefinitionConverter() }
            });

            return definitions;
        }
    }

    public class DefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var b = objectType == typeof(IDataStore);
            
            return b;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var newSerializer = new JsonSerializer();

            var dictionary = newSerializer.Deserialize<SqlDataStore>(reader);

            var dataStore = newSerializer.Deserialize<SqlDataStore>(reader);

            return dataStore;
            //return new SqlDataStore();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    //public class DataSourceConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        var b = objectType == typeof(IDataStore);

    //        return b;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        return new SqlDataStore();
    //    }

    //    public override bool CanWrite
    //    {
    //        get { return false; }
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
