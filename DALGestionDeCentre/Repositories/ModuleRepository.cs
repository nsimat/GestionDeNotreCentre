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
    public class ModuleRepository : IRepository<Module, int>
    {

        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

        public Module Insert(Module entity)
        {
            Command command = new Command("sp_InsertAModule", true);
            command["Nom"] = entity.Nom;
            command["Description"] = entity.DescriptionModule;
            command["TableDeMatieres"] = entity.TableDeMatieres;
            command["PrixJournalier"] = entity.PrixJournalier;
            command["NombreJours"] = entity.NombreJours;
            command["NbreJoursAffectes"] = entity.NbreJoursAffectes;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdModule = lastId;
            return entity;
        }
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAModule");
            command.AddParameter("IdModule", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }


        public IEnumerable<Module> Get()
        {
            Command command = new Command("SELECT * FROM Module WHERE IdModule = @IdModule");
            return _Connection.ExecuteReader(command, dr => new Module().From(dr));
        }






        public Module Get(int id)
        {
            Command command = new Command("SELECT * FROM Module where IdModule = @IdModule");
            command["IdModule"] = id;
            return _Connection.ExecuteReader(command, dr => new Module().From(dr)).FirstOrDefault();
        }

        public bool Put(Module entity, int id)
        {
            Command command = new Command("sp_UpdateAModule");
            return _Connection.ExecuteNonQuery(command) == 1;
        }

       
    }
}