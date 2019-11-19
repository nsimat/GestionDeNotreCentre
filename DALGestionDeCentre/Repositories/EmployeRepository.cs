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
    public class EmployeRepository : IRepository<Employe, int>
    {
        private readonly Connection _Connection;
        private readonly PersonneRepository personneRepository = new PersonneRepository();

        #region Les constructeurs

        public EmployeRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public EmployeRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAnEmploye", true);

            command.AddParameter("IdEmploye", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Employe> Get()
        {
            Command command = new Command("SELECT * FROM V_Employe;");

            return _Connection.ExecuteReader(command, dr => new Employe().From(dr));
        }

        public IEnumerable<Personne> GetEmployes()
        {
            Command command = new Command("SELECT * FROM V_Employe;");

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr));
        }

        public Employe Get(int id)
        {
            Command command = new Command("SELECT * FROM V_Employe WHERE IdEmploye = @IdEmploye;");

            command["IdEmploye"] = id;

            return _Connection.ExecuteReader(command, dr => new Employe().From(dr)).FirstOrDefault();
        }

        public Employe Insert(Employe entity)
        {
            Command command = new Command("sp_Employe", true);

            command["IdEmploye"] = entity.IdEmploye;
            

            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdEmploye = lastId;

            return entity;
        }

        public bool Put(Employe entity, int id)//Les données de l'employé se trouvent dans la table Personne
        {
            Personne personne = personneRepository.Get(entity.IdEmploye);
            return personneRepository.Put(personne, personne.IdPersonne);
        }
    }
}
