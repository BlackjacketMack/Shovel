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
            

            var batches = Math.Ceiling((double)exportCount / _definition.BatchSize);

            _communicator.WriteLine(exportCount + " records to export with " + batches + " batches.");

            for (int i = 0; i < batches; i++)
            {
                _communicator.Write("\rBatch " + i);

                var startRow = (_definition.BatchSize * i);
                var objs = _definition.SourceDataStore.Export(startRow, _definition.BatchSize);

                foreach (var obj in objs)
                {
                    _definition.DestinationDataStore.Import(obj);
                }
            }
        }
    }
}
