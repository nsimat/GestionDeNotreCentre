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
    public class EntrepriseRepository : IRepository<Entreprise, int>
    {
        private readonly Connection _Connection = new Connection(ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString,
                                                                 ConfigurationManager.ConnectionStrings["SqlServer"].ProviderName);
        // la méthode insert
        

        //la méthode delete
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        //Get
        public IEnumerable<Entreprise> Get()
        {
            Command command = new Command("SELECT * FROM Entreprise;");

            return _Connection.ExecuteReader(command, dr => new Entreprise().From(dr));
        }

        public Entreprise Get(int id)
        {
            Command command = new Command("SELECT * FROM Entreprise WHERE IdEntreprise = @IdEntreprise;");
            command["IdEntreprise"] = id;

            return _Connection.ExecuteReader(command, dr => new Entreprise().From(dr)).FirstOrDefault();
        }




        public Entreprise Insert(Entreprise entity)
        {
            Command command = new Command("sp_Entreprise", true);
            command["NomEntreprise"] = entity.NomEntreprise;
            command["Rue"] = entity.Rue;
            command["Ville"] = entity.Ville;
            command["CodePostal"] = entity.CodePostal;
            command["Pays"] = entity.Pays;
            command["NumeroTelephone"] = entity.NumeroTelephone;
            int lastId = _Connection.ExecuteNonQuery(command);
            entity.IdEntreprise = lastId;
            return entity;
        }

        public bool Put(Entreprise entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}