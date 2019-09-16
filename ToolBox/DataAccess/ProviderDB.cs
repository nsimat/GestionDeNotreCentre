using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataAccess
{
   public static class ProviderDB
    {
        public static string SqlClient { get; } = "System.Data.SqlClient";
        public static string OracleClient { get; } = "System.Data.OracleClient";
        public static string OdbcClient { get; } = "System.Data.OdbcClient";
        public static string OleDbClient { get; } = "System.Data.OleDb";
    }
}
