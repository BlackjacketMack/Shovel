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
using System.IO;


using System.Configuration;namespace Sql2Mongo.Command
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sql2Mongo = new Sql2Mongo();


            var definition = File.ReadAllText(ConfigurationManager.AppSettings["DefinitionFile"]);

            sql2Mongo.Process(null);


            Console.Write("ok");
            Console.ReadLine();
        }
    }
}
