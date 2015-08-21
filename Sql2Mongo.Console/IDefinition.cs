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

namespace Shovel.Command
{
    internal interface IDefinition
    {
        string Name { get; set; }

        IDataStore SourceDataStore { get;}
        IDataStore DestinationDataStore { get;}
    }
}
