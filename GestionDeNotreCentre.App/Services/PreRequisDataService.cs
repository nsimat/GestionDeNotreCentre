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
    public class PreRequisDataService : IDataService<PreRequis, int>
    {
        #region Les champs

        private readonly PreRequisRepository repository;

        #endregion
        #region Le constructeur

        public PreRequisDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

            repository = new PreRequisRepository(connection);
            
        }

        #endregion

        #region Les méthodes

        public PreRequis CreateElement(PreRequis element)
        {
            return repository.Insert(element);
        }

        public bool DeleteElement(PreRequis element)
        {
            return repository.Delete(element.IdModulePreRequis, element.IdModule);
        }

        public List<PreRequis> GetAllElements()
        {
            return repository.Get().ToList();
        }

        public List<PreRequis> GetPreRequisOfModule(int idModule)
        {
            return repository.GetPreRequis(idModule).ToList();
        }

        public PreRequis GetElementDetail(int elementIdModulePreRequis, int elementIdModule)
        {
            return repository.Get(elementIdModulePreRequis, elementIdModule);
        }

        public PreRequis GetElementDetail(int elementId)
        {
            return null;
        }

        public bool UpdateElement(PreRequis element)
        {
            return repository.Put(element, element.IdModule);
        }

        #endregion
    }
}
