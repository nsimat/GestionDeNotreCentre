using GestionDeNotreCentre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ToolBox.DataAccess;
using ToolBox.Repositories;

namespace GestionDeNotreCentre.Repositories
{
    public class PersonneRepository : IRepository<Personne, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

        public Personne Insert(Personne entity)
        {
            Command command = new Command("sp_Personne", true);

            command["NumeroRegistre"] = entity.NumeroRegistre;
            command["Nom"] = entity.Nom;
            command["Prenom"] = entity.Prenom;
            command["Email"] = entity.Email;
            command["Rue"] = entity.Rue;
            command["Ville"] = entity.Ville;
            command["CodePostal"] = entity.CodePostal;
            command["Pays"] = entity.Pays;
            command["NumeroTelephone"] = entity.NumeroTelephone;
            command["CV"] = entity.personCV;
            command["UserLogin"] = entity.UserLogin;
            command["MotDePasse"] = entity.MotDePasse;

            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdPersonne = lastId;

            return entity;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Personne Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Personne> Get()//à modifier
        {
            Command command = new Command("SELECT * FROM V_Personne;");

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr));
        }

        public bool Put(Personne entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}