using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Sql2Mongo.Command
{
    class Program
    {
        static void Main(string[] args)
        {

            var def = "{User:{UserID:@UserID,UserName:\"@UserName\"}}";

            var items = new[] { new{UserID=4,UserName="Rob"} };

            var properties = !items.Any() ? Enumerable.Empty<PropertyInfo>() : items.First().GetType().GetProperties().Cast<PropertyInfo>();

            var itemsFiltered = items.Select(s => getJsonDoc(s, def, properties));

            Console.Write(String.Concat(itemsFiltered));
            Console.ReadLine();
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
    }
}
