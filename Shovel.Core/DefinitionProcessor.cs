using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading;

namespace Shovel.Core
{
    public class DefinitionProcessor
    {
        private const int RETRY_COUNT = 3;
        private const int RETRY_WAITINSECONDS = 30;
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
            

            var batchNumbers = (int)Math.Ceiling((double)exportCount / _definition.BatchSize);

            _communicator.WriteLine("Process started at " + DateTime.UtcNow.ToLongDateString());
            _communicator.WriteLine(exportCount + " records to export with " + batchNumbers + " batches.");

            Parallel.For(0,
                            batchNumbers,
                            new ParallelOptions { MaxDegreeOfParallelism = _definition.MaxDegreeOfParallelism },
                            //bn => exportAndImport(bn));
                            bn => retry(() => exportAndImport(bn), RETRY_COUNT));
        }

        private void retry(Action original, int retryCount)
        {
            while (true)
            {
                try
                {
                    original();
                    return;
                }
                catch (Exception e)
                {
                    if (retryCount == 0)
                    {
                        throw;
                    }
                    // TODO: Logging
                    Thread.Sleep(TimeSpan.FromSeconds(RETRY_WAITINSECONDS));

                    retryCount--;
                }
            }
        }

        private void exportAndImport(int batchNumber)
        {
            var startRow = (_definition.BatchSize * batchNumber);

            var objs = _definition.SourceDataStore.Export(startRow, _definition.BatchSize);

            _definition.DestinationDataStore.Import(objs);

            _processedRecords += objs.Count();

            _communicator.Write("\rRecords Processed:" + _processedRecords);
        }
    }
}
