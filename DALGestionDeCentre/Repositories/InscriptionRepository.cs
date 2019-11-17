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
    public class InscriptionRepository : IRepository<Inscription, int>
    {
        #region Les champs

        private readonly Connection _Connection;

        #endregion

        #region Les constructeurs

        public InscriptionRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public InscriptionRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAnInscription", true);

            command.AddParameter("IdInscription", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Inscription> Get()
        {
            Command command = new Command("SELECT * FROM V_Inscription;");

            return _Connection.ExecuteReader(command, dr => new Inscription().From(dr));
        }

        public Inscription Get(int id)
        {
            Command command = new Command("SELECT * FROM V_Inscription WHERE IdInscription = @IdInscription;");

            command["IdInscription"] = id;

            return _Connection.ExecuteReader(command, dr => new Inscription().From(dr)).FirstOrDefault();
        }

        public Inscription Insert(Inscription entity)
        {
            Command command = new Command("sp_InsertAnInscription", true);
            command["DateInscription"] = entity.DateInscription;
            //command["DateValidation"] = entity.DateValidation;
            command["IdInstanceFormation"] = entity.IdInstanceFormation;
            command["IdStagiaire"] = entity.IdStatigiaire;
            command["IdEmploye"] = entity.IdEmploye;            

            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdInscription = lastId;
            return entity;           
        }

        public bool Put(Inscription entity, int id)
        {
            Command command = new Command("sp_UpdateAnInscription", true);

            command.AddParameter("IdInscription", entity.IdInscription);
            command.AddParameter("DateInscription", entity.DateInscription);
            command.AddParameter("DateValidation", entity.DateValidation);
            command.AddParameter("IdInsctanceFormation", entity.IdInstanceFormation);
            command.AddParameter("IdStagiaire", entity.IdStatigiaire);
            command.AddParameter("IdEmploye", entity.IdEmploye);
            
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        #endregion
    }
}
