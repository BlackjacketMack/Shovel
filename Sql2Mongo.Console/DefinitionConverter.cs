using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shovel.Core;

namespace Shovel.Command
{
    public class DefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var b = objectType == typeof(IDataStore);

            return b;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var dataStoreType = (DataStoreTypes)Enum.Parse(typeof(DataStoreTypes),(string)jo["DataStoreType"]);

            IDataStore dataStore = null;

            if (dataStoreType == DataStoreTypes.SqlDataStore)
            {
                dataStore = jo.ToObject<SqlDataStore>();
            }
            else if (dataStoreType == DataStoreTypes.MongoDataStore)
            {
                dataStore = jo.ToObject<MongoDataStore>();
            }

            return dataStore;
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
}
