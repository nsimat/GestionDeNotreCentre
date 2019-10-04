using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;
using ToolBox.Repositories;

namespace GestionDeCentreDAL.Repositories
{
    class TchatRepository : IRepository<Tchat, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);  
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteATchat");
            command.AddParameter("IdTchat", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Tchat> Get()
        {
            Command command = new Command("SELECT * FROM V_Tchat WHERE IdTchat = @IdTchat");
            return _Connection.ExecuteReader(command, dr => new Tchat().From(dr));
        }

        public Tchat Get(int id)
        {
            Command command = new Command("SELECT * FROM Tchat WHERE IdTchat = @IdTchat");
            command["IdTchat"] = id;
            return _Connection.ExecuteReader(command, dr => new Tchat().From(dr)).FirstOrDefault();
        }

        public Tchat Insert(Tchat entity)
        {
            Command command = new Command("sp_InsertATchat", true);
            command["MessageTchat"] = entity.MessageTchat;
            command["DateDebut"] = entity.DateDebut;
            command["DateCloture"] = entity.DateCloture;
            command["IdEnvoyeur"] = entity.NumeroRegistreEnvoyeur;
            command["IdRecepteur"] = entity.NumeroRegistreRecepteur;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdTchat = lastId;
            return entity;
        }

        public bool Put(Tchat entity, int id)
        {
            Command command = new Command("sp_UpdateATchat");
            return _Connection.ExecuteNonQuery(command) == 1;
        }
    }
}
