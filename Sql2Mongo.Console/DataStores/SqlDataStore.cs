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
using Dapper;
using System.Data.SqlClient;
using Shovel.Core;

namespace Shovel.Command
{
    internal class SqlDataStore : BaseDataStore, IDataStore
    {
        public long ExportCountTotal()
        {
            var countSql = "SELECT Count_Big(*) AS ExportCount FROM (" + this.ExportQuery + ") t";

            var count = query(countSql).First();

            return (long)count.ExportCount;
        }

        private IEnumerable<dynamic> query(string sql,object parameters = null)
        {
            IEnumerable<object> q;
            using (var dbConnection = new SqlConnection(this.GetConnectionString()))
            {
                q = dbConnection.Query(sql, parameters).ToList();
            }

            return q;
        }

        public IEnumerable<object> Export(long startRow, long maxRows)
        {
            var exportSql = "SELECT * FROM (" + this.ExportQuery + ") t ORDER BY 1 OFFSET @startRow ROWS FETCH NEXT @maxRows ROWS ONLY";

            var q = query(exportSql, new { startRow, maxRows });

            return q;
        }

        private string formatQuery()
        {
            return this.ExportQuery;
        }

        public void Import(IEnumerable<object> objs)
        {
            throw new NotImplementedException();
        }
    }
}
