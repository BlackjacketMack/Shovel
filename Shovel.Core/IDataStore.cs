using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Shovel.Core
{
    public interface IDataStore
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
