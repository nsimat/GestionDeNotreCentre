using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Services
{
    public class ModuleDataService : IDataService<Module, int>
    {
        private readonly ModuleRepository repository;

        public ModuleDataService(ModuleRepository repository)
        {
            this.repository = repository;
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

        public bool UpdateElement(Module element)
        {
            return repository.Put(element, element.IdModule);
        }       
    }
}
