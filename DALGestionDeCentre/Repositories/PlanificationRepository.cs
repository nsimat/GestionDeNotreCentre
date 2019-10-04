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
    class PlanificationRepository : IRepository<Planification, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
            ConfigurationManager.ConnectionStrings["GestionDeCentre"].ConnectionString);
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAPlanification");
            command.AddParameter("IdEntreprise", id);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Planification> Get()
        {
            throw new NotImplementedException();
        }

        public Planification Get(int id)
        {
            throw new NotImplementedException();
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
    }
}
