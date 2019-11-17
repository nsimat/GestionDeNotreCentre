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
    public class FormateurRepository : IRepository<Formateur, int>
    {
        private readonly Connection _Connection;

        #region Les constructeurs

        public FormateurRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public FormateurRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAFormateur", true);

            command.AddParameter("IdFormateur", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public IEnumerable<Formateur> Get()
        {
            Command command = new Command("SELECT * FROM Formateur");
            return _Connection.ExecuteReader(command, dr => new Formateur().From(dr));
        }

        public Formateur Get(int id)
        {
            Command command = new Command("SELECT * FROM Formateur WHERE IdFormateur = @IdFormateur;");

            command["IdFormateur"] = id;

            return _Connection.ExecuteReader(command, dr => new Formateur().From(dr)).FirstOrDefault();
        }

        public Formateur Insert(Formateur entity)
        {
            Command command = new Command("sp_InsertAFormateur", true);
            command["IdFormateur"] = entity.IdFormateur;
            
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdFormateur = lastId;
            return entity;
        }

        public bool Put(Formateur entity, int id)
        {
            Command command = new Command("sp_UpdateAFormateur", true);
            command.AddParameter("IdFormateur", entity.IdFormateur);
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        #endregion
    }
}
