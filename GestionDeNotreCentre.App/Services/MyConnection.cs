using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;

namespace GestionDeNotreCentre.App.Services
{
    public class MyConnection
    {
        public string ConnectionString { get; set; }

        public string Provider { get; set; }

        public Connection AppConnection 
        {
            get { return new Connection(ConnectionString, Provider); }             
        }

        public MyConnection()
        {
            ConnectionString = @"Data Source=MIKELENOVO\SQLEXPRESS;Initial Catalog=GestionDeCentreBD;Integrated Security=True;
                                 Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;
                                 Encrypt=False;TrustServerCertificate=True";
            Provider = "System.Data.SqlClient";
        }
    }
}
