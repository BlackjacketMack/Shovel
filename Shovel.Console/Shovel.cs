using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.IO;


using System.Configuration;
using Newtonsoft.Json;
using Shovel.Core;
namespace Shovel.Command
{
    public class ShovelConsole
    {
        static void Main(string[] args)
        {
            IEnumerable<Definition> definitions = Enumerable.Empty<Definition>();

            try
            {
                definitions = initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to initialize application.");
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("Y'all ready for this?");
            Console.WriteLine("                     ");
            Console.WriteLine("                 /-----\\");
            Console.WriteLine("|--|             |       \\");
            Console.WriteLine("|  |=============|        >");
            Console.WriteLine("|--|             |       /");
            Console.WriteLine("                 \\-----/");
            Console.WriteLine("(press enter)");
            Console.ReadLine();


            foreach (var definition in definitions)
            {
                processDefinition(definition);
            }


            Console.Write("ok");
            Console.ReadLine();
        }

        private static IEnumerable<Definition> initialize()
        {
            var definitionPath = ConfigurationManager.AppSettings["DefinitionFile"];

            if(definitionPath == null)
            {
                throw new ApplicationException("'DefinitionFile' must be specified.");
            }

            var definitionPathFull = Path.GetFullPath(definitionPath);

            var definitionsJson = File.ReadAllText(definitionPathFull);

            var definitions = deserializeDefinitions(definitionsJson);

            return definitions;
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
            var logFile = ConfigurationManager.AppSettings["LogFile"];
            if (logFile == null) throw new ApplicationException("LogFile not specified.");

            logFile = Path.GetFullPath(logFile);

            var logger = new Logger(logFile);

            var definitionProcessor = new DefinitionProcessor(definition, logger);
            definitionProcessor.Process();
        }
    }
}
