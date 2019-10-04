using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Repositories;

namespace GestionDeCentreDAL.Repositories
{
    class CompositionRepository : IRepository<Composition, int>
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Composition> Get()
        {
            throw new NotImplementedException();
        }

        public Composition Get(int id)
        {
            throw new NotImplementedException();
        }

        public Composition Insert(Composition entity)
        {
            throw new NotImplementedException();
        }

        public bool Put(Composition entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
