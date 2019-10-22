using GestionDeCentreDAL.Services;
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
            command["CV"] = entity.PersonCV;
            command["UserLogin"] = entity.UserLogin;
            command["MotDePasse"] = entity.MotDePasse;

            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdPersonne = lastId;

            return entity;
        }

        public bool Delete(int id)
        {
            Command command = new Command("sp_DeleteAPersonne", true);

            command.AddParameter("IdPersonne", id);

            return _Connection.ExecuteNonQuery(command) == 1;
        }

        public Personne Get(int id)
        {
            Command command = new Command("SELECT * FROM V_Personne WHERE IdPersonne = @IdPersonne;");

            command["IdPersonne"] = id;

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr)).FirstOrDefault();
        }

        public Personne Get(string idString)
        {

            if (Int32.TryParse(idString, out int idPersonne))
                return Get(idPersonne);
            return null;
        }

        public IEnumerable<Personne> Get()
        {
            Command command = new Command("SELECT * FROM V_Personne;");

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr));//à modifier?
        }

        public Personne Authentifier(string userLogin, string motDePasse)
        {
            string motDePasseEncode = HashPassword.Hash(motDePasse).Substring(0,25);
            Command command = new Command("SELECT * FROM V_Personne WHERE UserLogin LIKE @UserLogin AND MotDePasse LIKE @MotdePasse;");

            command.AddParameter("UserLogin", userLogin);
            command.AddParameter("MotDePasse", motDePasseEncode);

            return _Connection.ExecuteReader(command, dr => new Personne().From(dr)).FirstOrDefault();
        }

        //public Personne Get(string userLogin)
        //{
        //    Command command = new Command("SELECT * FROM V_Personne WHERE UserLogin=@UserLogin;");

        //    command["UserLogin"] = userLogin;
        //    return _Connection.ExecuteReader(command, dr => new Personne().From(dr)).FirstOrDefault();
        //}

        public bool Put(Personne entity, int id)
        {
            Command command = new Command("sp_UpdateAPersonne", true);

            command.AddParameter("IdPersonne", entity.IdPersonne);
            command.AddParameter("NumeroRegistre", entity.NumeroRegistre);
            command.AddParameter("Nom", entity.Nom);
            command.AddParameter("Prenom", entity.Prenom);
            command.AddParameter("Email", entity.Email);
            command.AddParameter("Rue", entity.Rue);
            command.AddParameter("Ville", entity.Ville);

            command["CodePostal"] = entity.CodePostal;
            command["Pays"] = entity.Pays;
            command["NumeroTelephone"] = entity.NumeroTelephone;
            command["CV"] = entity.PersonCV;
            command["UserLogin"] = entity.UserLogin;
            command["MotDePasse"] = entity.MotDePasse;
            command["IdEntreprise"] = entity.IdEntreprise;

            return _Connection.ExecuteNonQuery(command) == 1;
        }
    }
}