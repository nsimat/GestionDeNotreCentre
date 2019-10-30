using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.DataAccess;

namespace GestionDeNotreCentre.App.Services
{
    public class LoginDataService
    {
        private readonly PersonneRepository repository;
        public LoginDataService()
        {
            
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            repository = new PersonneRepository(connection);
        }

        public Personne Authentifier(string userLogin, string motDePasse)
        {
            return repository.Authentifier(userLogin, motDePasse);            
        }
    }
}
