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
    public class ModuleDataService : IDataService<Module, int>
    {
        private readonly ModuleRepository repository;

        public ModuleDataService()
        {
            Connection connection = new Connection(ConfigurationManager.ConnectionStrings["GestionCentre"].ConnectionString,
                                                                ConfigurationManager.ConnectionStrings["GestionCentre"].ProviderName);
            repository = new ModuleRepository(connection);
        }

        public Module CreateElement(Module element)
        {
            return repository.Insert(element);
        }

        public bool DeleteElement(Module element)
        {
            return repository.Delete(element.IdModule);
        }

        public List<Module> GetAllElements()
        {
            return repository.Get().ToList();
        }

        public Module GetElementDetail(int elementId)
        {
            return repository.Get(elementId);
        }

        public Module GetModuleByName(string moduleName)
        {
            return repository.GetModule(moduleName);
        }

        public bool UpdateElement(Module element)
        {
            return repository.Put(element, element.IdModule);
        }       
    }
}
