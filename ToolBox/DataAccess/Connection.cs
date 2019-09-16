using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataAccess
{
    public class Connection : IConnection
    {
        protected string _connectionString { get; private set; }
        protected DbProviderFactory ClientDB { get; private set;}

        /// <summary>
        /// Cree la connection avec la Base de donnés
        /// </summary>
        /// <param name="connectionString">String de la connection </param>
        /// <param name="ClientDB">Choix :"SqlClient" , "OracleClient", "OdbcClient" ,"OleDbClient"</param>
        public Connection(string connectionString , string ClientDB)
        {
            if (string.IsNullOrWhiteSpace(connectionString)|| string.IsNullOrWhiteSpace(ClientDB) )
                throw new ArgumentException("La connexion ne peut pas etre vide");
            this._connectionString = connectionString;
            this.ClientDB = DbProviderFactories.GetFactory(ClientDB);
        }

        public object ExecuteScalar(Command Command)
        {
            using (DbConnection connection = this.CreateConnection())
            {
                using (DbCommand command = this.CreateCommand(Command, connection))
                {
                    connection.Open();
                    object obj = command.ExecuteScalar();
                    connection.Close();
                    return obj is DBNull ? (object)null : obj;
                }
            }

        }

        private DbCommand CreateCommand(Command Command, DbConnection Connection)
        {
            DbCommand command = Connection.CreateCommand();
            command.CommandText = Command.Query;
            command.CommandType = Command.IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
            foreach (KeyValuePair<string, object> kvp in (IEnumerable<KeyValuePair<string, object>>)Command.Parameters)
            {
                DbParameter Parameter = this.ClientDB.CreateParameter();
                Parameter.ParameterName = kvp.Key;
                Parameter.Value = kvp.Value ?? (object)DBNull.Value;
                command.Parameters.Add((object)Parameter);
            }
            return command;
        }

        public int ExecuteNonQuery(Command Command)
        {
            using (DbConnection connection = this.CreateConnection())
            {
                using (DbCommand command = this.CreateCommand(Command, connection))
                {
                    connection.Open();
                   
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }

        public IEnumerable<TResult>ExecuteReader<TResult>(Command Command , Func<IDataRecord , TResult> Selector)
        {
            using (DbConnection c = this.CreateConnection())
            {
                using (DbCommand cmd = this.CreateCommand(Command, c))
                {
                    c.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return Selector(reader);
                    }
                    c.Close();
                }
            }
        }
        private DbConnection CreateConnection()
        {
            DbConnection connection = this.ClientDB.CreateConnection();
            connection.ConnectionString = this._connectionString;
            return connection;
        }
    
   
     
    }
}
