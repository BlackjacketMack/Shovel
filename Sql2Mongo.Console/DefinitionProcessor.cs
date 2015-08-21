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
        private ICommunicator _communicator;

        public DefinitionProcessor(Definition definition,ICommunicator communicator)
        {
            _definition = definition;
            _communicator = communicator;
        }

        public void Process()
        {
            _communicator.WriteLine("Beginning process: Source " + _definition.SourceDataStore.ConnectionStringName + ", Destination " + _definition.DestinationDataStore.ConnectionStringName);

            var exportCount = _definition.SourceDataStore.ExportCountTotal();
            _communicator.WriteLine(exportCount + " records to export.");

            var objs = _definition.SourceDataStore.Export();

            //foreach (var obj in objs)
            //{
            //    _definition.DestinationDataStore.Import(obj);
            //}
        }
    }
}
