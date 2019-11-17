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
    public class CompositionRepository : IRepository<Composition, int>
    {
        private readonly Connection _Connection;

        public CompositionRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public CompositionRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #region Les méthodes

        public bool Delete(int idFormation, int idModule)
        {
            Command command = new Command("sp_DeleteAComposition", true);

            command.AddParameter("IdFormation", idFormation);
            command.AddParameter("IdModule", idModule);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id)//inutile
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Composition> Get()
        {
            Command command = new Command("SELECT * FROM V_Composition;");

            return _Connection.ExecuteReader(command, dr => new Composition().From(dr));
        }

        public IEnumerable<Composition> GetFormationCompositions(int idFormation)
        {
            Command command = new Command("SELECT * FROM V_Composition WHERE IdFormation = @IdFormation;");
            command["IdFormation"] = idFormation;

            return _Connection.ExecuteReader(command, dr => new Composition().From(dr));
        }

        public Composition Get(int idFormation, int idModule)
        {
            Command command = new Command("SELECT * FROM V_Composition WHERE IdFormation = @IdFormation AND IdModule = @IdModule;");

            command["IdFormation"] = idFormation;
            command["IdModule"] = idModule;

            return _Connection.ExecuteReader(command, dr => new Composition().From(dr)).FirstOrDefault();
        }

        public Composition Get(int id)//inutile
        {
            throw new NotImplementedException();
        }

        public Composition Insert(Composition entity)//à revoir
        {
            Command command = new Command("sp_Composition", true);

            command["IdFormation"] = entity.IdFormation;
            command["IdModule"] = entity.IdModule;
            command["DateAjout"] = entity.DateAjout;
            command["DateSuppression"] = entity.DateSuppression;

            int lastId = _Connection.ExecuteNonQuery(command);
            
            return entity;
        }

        public bool Put(Composition entity, int id)
        {
            Command command = new Command("sp_UpdateAComposition", true);

            command.AddParameter("IdFormation", entity.IdFormation);
            command.AddParameter("IdModule", entity.IdModule);
            command.AddParameter("DateAjout", entity.DateAjout);
            command.AddParameter("DateSuppression", entity.DateSuppression);            

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        #endregion
    }
}
