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
    public class FormationDataService : IDataService<Formation, int>
    {
        #region Les champs

        private readonly FormationRepository repository;

        #endregion

        #region Le constructeur

        public FormationDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            repository = new FormationRepository(connection);
        }

        #endregion

        #region Les méthodes

        public Formation CreateElement(Formation element)
        {
            return repository.Insert(element);
        }

        public bool DeleteElement(Formation element)
        {
            return repository.Delete(element.IdFormation);
        }

        public List<Formation> GetAllElements()
        {
            return repository.Get().ToList();
        }

        public Formation GetElementDetail(int elementId)
        {
            return repository.Get(elementId);
        }

        public Formation GetFormationByName(string nom)
        {
            return repository.Get(nom);
        }

        public bool UpdateElement(Formation element)
        {
            return repository.Put(element, element.IdFormation);
        }

        public void EstimationOfDays(Formation formation)
        {
            repository.EstimateTrainingDaysNumber(formation);
        }

        #endregion
    }
}
