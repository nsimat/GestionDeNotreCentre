using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Services
{
    public class CompetenceDataService : IDataService<Competence, int>
    {
        private readonly CompetenceRepository competenceRepository;

        public Competence CreateElement(Competence element)
        {
            return competenceRepository.Insert(element);
        }

        public bool DeleteElement(Competence element)
        {
            return competenceRepository.DeleteCompentence(element.IdFormateur, element.IdModule);
        }

        public List<Competence> GetAllElements()
        {
            return competenceRepository.Get().ToList();
        }

        public Competence GetElementDetail(int elementId)
        {
            throw new NotImplementedException();
        }

        public Competence GetElementDetail(int elementId1, int elementId2)
        {
            return competenceRepository.Get(elementId1, elementId2);
        }

        public bool UpdateElement(Competence element)
        {
            return competenceRepository.Put(element, element.IdFormateur);
        }
    }
}
