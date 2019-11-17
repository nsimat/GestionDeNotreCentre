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
    public class PersonneDataService : IDataService<Personne, int>
    {
        private readonly PersonneRepository repository;

        public PersonneDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            repository = new PersonneRepository(connection);
        }
                

        public Personne CreateElement(Personne element)
        {
            return repository.Insert(element);
        }

        public bool DeleteElement(Personne element)
        {
            return repository.Delete(element.IdPersonne);
        }

        public List<Personne> GetAllElements()
        {
            return repository.Get().ToList();
        }

        public Personne GetElementDetail(int elementId)
        {
            return repository.Get(elementId);
        }

        public bool UpdateElement(Personne element)
        {
            return repository.Put(element, element.IdPersonne);
        }
    }
}
