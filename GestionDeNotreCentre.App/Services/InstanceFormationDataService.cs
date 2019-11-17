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
    class InstanceFormationDataService : IDataService<InstanceFormation, int>
    {
        private readonly InstanceFormationRepository instanceFormationRepository;
        private readonly FormationRepository formationRepository;
        private readonly PersonneRepository personneRepository;

        public InstanceFormationDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            instanceFormationRepository = new InstanceFormationRepository(connection);
        }        

        public InstanceFormation CreateElement(InstanceFormation element)
        {
            return instanceFormationRepository.Insert(element);
        }

        public bool DeleteElement(InstanceFormation element)
        {
            return instanceFormationRepository.Delete(element.IdInstanceFormation);
        }

        public List<InstanceFormation> GetAllElements()
        {
            return instanceFormationRepository.Get().ToList();
        }

        public InstanceFormation GetElementDetail(int elementId)
        {
            return instanceFormationRepository.Get(elementId);
        }

        public bool UpdateElement(InstanceFormation element)
        {
            return instanceFormationRepository.Put(element, element.IdInstanceFormation);
        }
    }
}
