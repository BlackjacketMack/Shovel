﻿using System;
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

namespace Sql2Mongo.Command
{
    internal class SqlDataStore : BaseDataStore, IDataStore
    {
        public string ExportQuery { get; set; }
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

        public IEnumerable<object> Export()
        {
            var queryFormatted = formatQuery();

            IEnumerable<object> q;
            using (var dbConnection = new SqlConnection(this.GetConnectionString()))
            {
                q = dbConnection.Query(queryFormatted).ToList();
            }

            return q;
        }

        private string formatQuery()
        {
            return this.ExportQuery;
        }

        public void Import(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
