using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Shovel.Core
{
    public interface IDefinition
    {
        string Name { get; set; }

        IDataStore SourceDataStore { get;}
        IDataStore DestinationDataStore { get;}
    }
}
