using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ToolBox.DataAccess;
using ToolBox.Repositories;

namespace GestionDeCentreDAL.Repositories
{
    public class FormationRepository : IRepository<Formation, int>

    {
        private readonly Connection _Connection;

        #region Les constructeurs
        public FormationRepository()
        {
            _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
        }

        public FormationRepository(Connection connection)
        {
            if (_Connection == null)
                _Connection = connection;
        }

        #endregion

        #region Les méthodes

        public Formation Insert(Formation entity)
        {
            Command command = new Command("sp_Formation", true);
            command["Nom"] = entity.Nom;
            command["DescriptionFormation"] = entity.DescriptionFormation;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdFormation = lastId;

            return entity;
            
        }        


        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAFormation", true);

            command.AddParameter("IdFormation", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }



        public IEnumerable<Formation> Get()
        {
            Command command = new Command("SELECT * FROM Formation;");
            return _Connection.ExecuteReader(command, dr => new Formation().From(dr));
        }

        public Formation Get(int id)
        {
            Command command = new Command("SELECT * FROM Formation WHERE IdFormation = @IdFormation");
            command.AddParameter("IdFormation", id);
            return _Connection.ExecuteReader(command, dr => new Formation().From(dr)).FirstOrDefault();
        }

        public Formation Get(string nom)
        {
            Command command = new Command("SELECT * FROM V_Formation WHERE Nom = @Nom");
            command.AddParameter("Nom", nom);
            return _Connection.ExecuteReader(command, dr => new Formation().From(dr)).FirstOrDefault();
        }

        public bool Put(Formation entity, int id)
        {
            Command command = new Command("sp_UpdateAFormation", true);
            command.AddParameter("IdFormation", id);
            command.AddParameter("Nom", entity.Nom);
            command.AddParameter("DescriptionFormation", entity.DescriptionFormation);
            
            return _Connection.ExecuteNonQuery(command) == 1;
        }

        //public bool Update(Formation entity, int id)

        //{
        //    Command command = new Command("UPDATE Formation SET Nom = @Nom WHERE IdFormation = @Id ");
        //    command.AddParameter("IdFormation", id);
        //    command.AddParameter("Nom",entity.Nom);
        //    command.AddParameter("DescriptionFormation", entity.DescriptionFormation);
        //    return _Connection.ExecuteNonQuery(command) == 1;
        //}

        #endregion

    }
}