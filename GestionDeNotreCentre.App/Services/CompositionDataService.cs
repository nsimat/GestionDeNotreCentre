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
    public class CompositionDataService : IDataService<Composition, int>
    {
        #region Les champs

        private readonly CompositionRepository compositionRepository;
        private readonly ModuleRepository moduleRepository;
        private readonly FormationRepository formationRepository;

        #endregion

        #region Le constructeur

        public CompositionDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

            compositionRepository = new CompositionRepository(connection);
            moduleRepository = new ModuleRepository(connection);
            formationRepository = new FormationRepository(connection);
        }
        #endregion

        public Composition CreateElement(Composition element)
        {
            return compositionRepository.Insert(element);
        }

        public bool DeleteElement(Composition element)
        {
            return compositionRepository.Delete(element.IdFormation, element.IdModule);
        }

        public List<Composition> GetAllElements()
        {
            return compositionRepository.Get().ToList();
        }

        public List<Composition> GetCompositionsFrom(int idFormation)
        {
            return compositionRepository.GetFormationCompositions(idFormation).ToList();
        }

        public Composition GetElementDetail(int elementIdFormation, int elementIdModule)
        {
            return compositionRepository.Get(elementIdFormation, elementIdModule);
        }

        public Composition GetElementDetail(int elementId)//inutile
        {
            throw new NotImplementedException();
        }

        public bool UpdateElement(Composition element)
        {
            return compositionRepository.Put(element, element.IdFormation);
        }
    }
}
