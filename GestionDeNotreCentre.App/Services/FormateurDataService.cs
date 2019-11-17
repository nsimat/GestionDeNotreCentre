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
    public class FormateurDataService : IDataService<Formateur, int>
    {
        private readonly FormateurRepository formateurRepository;
        private readonly PersonneRepository personneRepository;

        public FormateurDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);

            formateurRepository = new FormateurRepository(connection);
            personneRepository = new PersonneRepository(connection);
        }

        public Formateur CreateElement(Formateur element)
        {
            return formateurRepository.Insert(element);
        }

        public bool DeleteElement(Formateur element)
        {
            return formateurRepository.Delete(element.IdFormateur);
        }

        public List<Formateur> GetAllElements()
        {
            var formateurs = formateurRepository.Get().ToList();

            foreach(var formateur in formateurs)
            {
                formateur.Personne = personneRepository.Get(formateur.IdFormateur);
            }

            return formateurs;
        }

        public Formateur GetElementDetail(int elementId)
        {
            var formateur =  formateurRepository.Get(elementId);
            formateur.Personne = personneRepository.Get(elementId);
            return formateur;
        }

        public bool UpdateElement(Formateur element)
        {
            return formateurRepository.Put(element, element.IdFormateur);
        }
    }
}
