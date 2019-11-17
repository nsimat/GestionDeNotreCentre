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
    public class PlanificationDataService : IDataService<Planification, int>
    {
        #region Les champs

        private readonly PlanificationRepository planificationRepository;
        private readonly InstanceFormationRepository instanceFormationRepository;
        private readonly FormateurRepository formateurRepository;
        private readonly ModuleRepository moduleRepository;

        #endregion

        #region Le constructeur

        public PlanificationDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

            planificationRepository = new PlanificationRepository(connection);
            instanceFormationRepository = new InstanceFormationRepository(connection);
            formateurRepository = new FormateurRepository(connection);
            moduleRepository = new ModuleRepository(connection);

        }

        #endregion


        #region les méthodes

        public Planification CreateElement(Planification element)
        {
            return planificationRepository.Insert(element);
        }

        public bool DeleteElement(Planification element)
        {
            return planificationRepository.Delete(element.IdPlanification);
        }

        public List<Planification> GetAllElements()
        {
            return planificationRepository.Get().ToList();
        }

        public Planification GetElementDetail(int elementId)
        {
            return planificationRepository.Get(elementId);
        }

        public bool UpdateElement(Planification element)
        {
            return planificationRepository.Put(element, element.IdPlanification);
        }

        #endregion
    }
}
