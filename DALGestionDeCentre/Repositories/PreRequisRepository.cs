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
    public class PreRequisRepository : IRepository<PreRequis, int>
    {
        #region Les champs

        private readonly Connection _Connection;

        #endregion

        #region Les constructeurs

        public PreRequisRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

        }

        public PreRequisRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public bool Delete(int idModulePreRequis, int idModule)
        {
            Command command = new Command("sp_DeleteAPreRequis", true);
            command.AddParameter("IdModulePrerequis", idModulePreRequis);
            command.AddParameter("IdModule", idModule);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id)
        {
            return true;
        }

        public IEnumerable<PreRequis> Get()
        {
            Command command = new Command("SELECT * FROM V_PreRequis;");
            return _Connection.ExecuteReader(command, dr => new PreRequis().From(dr));
        }

        public IEnumerable<PreRequis> GetPreRequis(int id)
        {
            Command command = new Command("SELECT * FROM V_PreRequis WHERE IdModule = @IdModule;");            
            command.AddParameter("IdModule", id);
            return _Connection.ExecuteReader(command, dr => new PreRequis().From(dr));
        }

        public PreRequis Get(int idModulePreRequis, int idModule)
        {
            Command command = new Command("SELECT * FROM V_PreRequis WHERE IdModulePrerequis = @IdModulePreRequis AND IdModule = @IdModule;");
            command.AddParameter("IdModulePrerequis", idModulePreRequis);
            command.AddParameter("IdModule", idModule);
            return _Connection.ExecuteReader(command, dr => new PreRequis().From(dr)).FirstOrDefault();
        }

        public PreRequis Get(int id)
        {
            return null;
        }

        public PreRequis Insert(PreRequis entity)
        {
            Command command = new Command("sp_InsertAPreRequis", true);
            command["IdModulePrerequis"] = entity.IdModulePreRequis;
            command["IdModule"] = entity.IdModule;
            
            int lastId = _Connection.ExecuteNonQuery(command);
            
            return entity;
        }

        public bool Put(PreRequis entity, int id)
        {
            //On ne peut mettre à jour un prérequis car il n'est constitué que de deux identifiants.
            return true;
        }

        #endregion
    }
}
