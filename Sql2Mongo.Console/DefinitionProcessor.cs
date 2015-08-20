using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Sql2Mongo.Command
{
    internal class DefinitionProcessor
    {
        private Definition _definition;
        public DefinitionProcessor(Definition definition)
        {
            _definition = definition;
        }

        public void Process()
        {
            _definition.DestinationDataStore.Insert(new { UserID = 4 });
        }
    }
}
