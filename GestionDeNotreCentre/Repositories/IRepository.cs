using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.Repositories
{
    interface IRepository<TEntity, TId> where TEntity : class where TId : struct
    {
        TEntity Create(TEntity entity);
        bool Delete(TId id);
        bool Update(TEntity entity, TId id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(TId id);
    }
}
