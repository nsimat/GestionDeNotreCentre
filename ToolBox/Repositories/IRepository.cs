using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Repositories
{
   public interface IRepository<TResult,TKey> where TResult : class where TKey : struct
    {
        IEnumerable<TResult> Get();
        TResult Get(TKey id);
        
        TResult Insert(TResult entity);
        bool Put(TResult entity,TKey id);
        bool Delete( TKey id);

    }
}
