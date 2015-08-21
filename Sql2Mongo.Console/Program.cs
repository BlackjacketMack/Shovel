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

            var definitionsJson = File.ReadAllText(ConfigurationManager.AppSettings["DefinitionFile"]);

            var definitions = deserializeDefinitions(definitionsJson);

            foreach (var definition in definitions)
            {
                processDefinition(definition);
            }


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

        private static void processDefinition(Definition definition)
        {
            var definitionProcessor = new DefinitionProcessor(definition, new Communicator());
            definitionProcessor.Process();
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
