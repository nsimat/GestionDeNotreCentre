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
    public class EmployeDataService : IDataService<Employe, int>
    {
        #region Les champs

        private readonly EmployeRepository employeRepository;

        #endregion

        #region Le constructeur

        public EmployeDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                            ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            employeRepository = new EmployeRepository(connection);
        }

        #endregion

        #region Les méthodes

        public Employe CreateElement(Employe element)
        {
            return employeRepository.Insert(element);
        }

        public bool DeleteElement(Employe element)
        {
            return employeRepository.Delete(element.IdEmploye);
        }

        public List<Employe> GetAllElements()
        {
            return employeRepository.Get().ToList();
        }

        public Employe GetElementDetail(int elementId)
        {
            return employeRepository.Get(elementId);
        }

        public bool UpdateElement(Employe element)
        {
            return employeRepository.Put(element, element.IdEmploye);
        }

        #endregion
    }
}
