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
        private ILogger _logger;

        private long _processedRecords;

        public DefinitionProcessor(Definition definition,ILogger communicator)
        {
            _definition = definition;
            _logger = communicator;
        }

        public void Process()
        {
            logIfEnabled("Process " + _definition.Name + " started at " + DateTime.Now.ToLongTimeString());
            _logger.WriteLine("Beginning process: Source " + _definition.SourceDataStore.ConnectionStringName + ", Destination " + _definition.DestinationDataStore.ConnectionStringName);

            var exportCount = _definition.SourceDataStore.ExportCountTotal();
            

            var batchNumbers = (int)Math.Ceiling((double)exportCount / _definition.BatchSize);

            _logger.WriteLine("Process started at " + DateTime.UtcNow.ToLongDateString());
            _logger.WriteLine(exportCount + " records to export with " + batchNumbers + " batches.");

            Parallel.For(0,
                            batchNumbers,
                            new ParallelOptions { MaxDegreeOfParallelism = _definition.MaxDegreeOfParallelism },
                            bn => retry(() => exportAndImport(bn), RETRY_COUNT));

            logIfEnabled("Process " + _definition.Name + " completed at " + DateTime.Now.ToLongTimeString());
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
                    logIfEnabled("Exception caught: current retries=" + retryCount);
                    logIfEnabled(e);

                    if (retryCount == 0)
                    {
                        throw;
                    }
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

            _logger.Write("\rRecords Processed:" + _processedRecords);
        }

        private void logIfEnabled(Exception ex)
        {
            logIfEnabled(ex.Message);
        }

        private void logIfEnabled(string message)
        {
            if (_definition.LoggingEnabled) 
            { 
                _logger.Log(message + System.Environment.NewLine);
            }
        }
    }
}
