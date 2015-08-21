﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Shovel.Command
{
    internal class DefinitionProcessor
    {
        private Definition _definition;
        private ICommunicator _communicator;

        private long _processedRecords;

        public DefinitionProcessor(Definition definition,ICommunicator communicator)
        {
            _definition = definition;
            _communicator = communicator;
        }

        public void Process()
        {
            _communicator.WriteLine("Beginning process: Source " + _definition.SourceDataStore.ConnectionStringName + ", Destination " + _definition.DestinationDataStore.ConnectionStringName);

            var exportCount = _definition.SourceDataStore.ExportCountTotal();
            

            var batches = (int)Math.Ceiling((double)exportCount / _definition.BatchSize);

            _communicator.WriteLine(exportCount + " records to export with " + batches + " batches.");

            Parallel.For(0,
                            batches,
                            new ParallelOptions { MaxDegreeOfParallelism = _definition.MaxDegreeOfParallelism },
                            i => { exportAndImport(i); });

            //for (int i = 0; i < batches; i++)
            //{
            //    exportAndImport(i);
            //}

        }

        private void exportAndImport(int batchNumber)
        {
            _processedRecords += _definition.BatchSize;

            _communicator.Write("\r Records Processed:" + _processedRecords);

            var startRow = (_definition.BatchSize * batchNumber);

            var objs = _definition.SourceDataStore.Export(startRow, _definition.BatchSize);
            
            _definition.DestinationDataStore.Import(objs);
        }
    }
}
