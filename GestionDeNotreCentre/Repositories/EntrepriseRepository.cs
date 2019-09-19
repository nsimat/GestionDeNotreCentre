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
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entreprise> Get()
        {
            Command command = new Command("SELECT * FROM V_Entreprise;");

            return _Connection.ExecuteReader(command, dr => new Entreprise().From(dr));
        }

        public Entreprise Get(int id)
        {
            Command command = new Command("SELECT * FROM V_Entreprise WHERE IdEntreprise = @IdEntreprise;");
            command["IdEntreprise"] = id;

            return _Connection.ExecuteReader(command, dr => new Entreprise().From(dr)).FirstOrDefault();
        }

        public Entreprise Insert(Entreprise entity)
        {
            throw new NotImplementedException();
        }

        public bool Put(Entreprise entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}