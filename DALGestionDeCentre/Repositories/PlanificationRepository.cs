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
    public class PlanificationRepository : IRepository<Planification, int>
    {
        #region Les champs

        private readonly Connection _Connection;

        #endregion

        #region Les constructeurs

        public PlanificationRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public PlanificationRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAPlanification");
            command.AddParameter("IdEntreprise", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Planification> Get()
        {
            Command command = new Command("SELECT * FROM V_Planification;");
            return _Connection.ExecuteReader(command, dr => new Planification().From(dr));
        }

        public Planification Get(int id)
        {
            Command command = new Command("SELECT * FROM V_Planification WHERE IdPlanification = @IdPlanification");
            command.AddParameter("IdPlanification", id);
            return _Connection.ExecuteReader(command, dr => new Planification().From(dr)).FirstOrDefault();
        }

        public Planification Insert(Planification entity)
        {
            Command command = new Command("sp_InsertAPlanification", true);
            command["DatePlanification"] = entity.DatePlanification;
            command["IdTypeJour"] = entity.IdTypeJour;
            command["IdFormateur"] = entity.Formateur;
            command["IdInstanceformation"] = entity.InstanceFormation;
            command["IdModule"] = entity.Module;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdPlanification = lastId;
            return entity;
        }

        public bool Put(Planification entity, int id)
        {
            Command command = new Command("sp_UpdateAPlanification");
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        #endregion
    }
}
