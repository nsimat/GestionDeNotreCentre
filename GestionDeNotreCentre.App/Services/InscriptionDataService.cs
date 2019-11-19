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
    public class InscriptionDataService : IDataService<Inscription, int>
    {
        #region Les champs

        private readonly InscriptionRepository inscriptionRepository;
        private readonly InstanceFormationRepository instanceFormationRepository;
        private readonly PersonneRepository personneRepository;
        private readonly EmployeRepository employeRepository;

        #endregion

        #region Le constructeur

        public InscriptionDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

            inscriptionRepository = new InscriptionRepository(connection);
            instanceFormationRepository = new InstanceFormationRepository(connection);
            personneRepository = new PersonneRepository(connection);
            employeRepository = new EmployeRepository(connection);

        }

        #endregion

        #region Les méthodes

        public Inscription CreateElement(Inscription element)
        {
            return inscriptionRepository.Insert(element);
        }

        public bool DeleteElement(Inscription element)
        {
            return inscriptionRepository.Delete(element.IdInscription);
        }

        public List<Inscription> GetAllElements()
        {
            return inscriptionRepository.Get().ToList();
        }

        public List<Inscription> GetInscriptionsFrom(InstanceFormation instanceFormation)
        {
            return inscriptionRepository.GetInscriptionsFrom(instanceFormation).ToList();
        }

        public Inscription GetElementDetail(int elementId)
        {
            return inscriptionRepository.Get(elementId);
        }

        public bool UpdateElement(Inscription element)
        {
            return inscriptionRepository.Put(element, element.IdInscription);
        }

        #endregion
    }
}
