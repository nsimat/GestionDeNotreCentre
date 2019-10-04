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
    class TacheRepository : IRepository<Tache, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteATache");
            command.AddParameter("IdTache", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Tache> Get()
        {
            Command command = new Command("SELECT * FROM V_Tache WHERE IdTache = @IdTache");
            return _Connection.ExecuteReader(command, dr => new Tache().From(dr));
        }

        public Tache Get(int id)
        {
            Command command = new Command("SELECT * FROM Tache WHERE IdTache = @IdTache");
            command["IdTache"] = id;
            return _Connection.ExecuteReader(command, dr => new Tache().From(dr)).FirstOrDefault();
        }

        public Tache Insert(Tache entity)
        {
            Command command = new Command("sp_InsertATache", true);
            command["LibelleTache"] = entity.LibelleTache;
            command["MessageTache"] = entity.MessageTache;
            command["DateCreation"] = entity.DateCreation;
            command["DateCloture"] = entity.DateCloture;
            command["IdCreateur"] = entity.Createur;
            command["IdRealisateur"] = entity.Realisateur;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdTache = lastId;
            return entity;

        }

        public bool Put(Tache entity, int id)
        {
            Command command = new Command("sp_UpdateATache");
            return _Connection.ExecuteNonQuery(command) == 1;
        }
    }
}
