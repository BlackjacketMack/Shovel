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
    internal interface IDataStore
    {
        string ConnectionStringName { get; set; }
        DataStoreTypes DataStoreType { get; set; }

        //exporting
        IEnumerable<object> Export(long startRow, long maxRows);
        long ExportCountTotal();
        string ExportQuery { get; set; }

        //importing
        void Import(IEnumerable<object> objs);
        string ImportQuery { get; set; }
    }
}
