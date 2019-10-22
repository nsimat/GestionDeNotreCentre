using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;
using ToolBox.Repositories;

namespace DALGestionDeCentre.Repositories
{
    public class EmployeRepository : IRepository<Employe, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAnEmploye", true);

            command.AddParameter("IdEmploye", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Employe> Get()
        {
            Command command = new Command("SELECT * FROM Employe;");

            return _Connection.ExecuteReader(command, dr => new Employe().From(dr));
        }

        public IEnumerable<Personne> GetEmployes()
        {
            Command command = new Command("SELECT * FROM V_Personne;");

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr));
        }

        public Employe Get(int id)
        {
            Command command = new Command("SELECT * FROM Employe WHERE IdEmploye = @IdEmploye;");

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

        public bool Put(Employe entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
