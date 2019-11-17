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
    public class CompetenceRepository : IRepository<Competence, int>
    {
        #region Les champs

        private readonly Connection _Connection;

        #endregion

        #region Les constructeurs

        public CompetenceRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                               ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public CompetenceRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public bool DeleteCompentence(int idFMT, int idMDL)
        {
            Command command = new Command("sp_DeleteACompetence", true);

            command.AddParameter("IdFormateur", idFMT);
            command.AddParameter("IdModule", idMDL);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competence> Get()
        {
            Command command = new Command("SELECT * FROM V_Competence;");

            return _Connection.ExecuteReader(command, dr => new Competence().From(dr));
        }

        public Competence Get(int formateurId, int moduleId)
        {
            Command command = new Command("SELECT * FROM V_Competence WHERE IdFormateur = @IdFormateur AND IdModule = @IdModule;");

            command["IdFormateur"] = formateurId;
            command["IdModule"] = moduleId;

            return _Connection.ExecuteReader(command, dr => new Competence().From(dr)).FirstOrDefault();
        }

        public Competence Get(int id)
        {
            throw new NotImplementedException();
        }

        public Competence Insert(Competence entity)
        {
            Command command = new Command("sp_InsertACompetence", true);
            command["IdFormateur"] = entity.IdFormateur;
            command["IdModule"] = entity.IdModule;

            int lastId = _Connection.ExecuteNonQuery(command);
            //entity.IdFormateur = lastId;//inutile
            return entity;
        }

        public bool Put(Competence entity, int id)
        {
            Command command = new Command("sp_UpdateACompetence", true);
            command.AddParameter("IdFormateur", entity.IdFormateur);
            command.AddParameter("IdModule", entity.IdModule);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        #endregion
    }
}
