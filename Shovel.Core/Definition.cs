using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Shovel.Core
{
    public class Definition
    {
        public string Name { get; set; }
        public int BatchSize { get; set; }
        public int MaxDegreeOfParallelism { get; set; }
        public bool LoggingEnabled { get; set; }

        public IDataStore SourceDataStore { get; set; }

        public IDataStore DestinationDataStore { get; set; }

        public Definition()
        {

        }
    }
}
