using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Services
{
    interface IDataService<TResult, TKey> where TResult : class where TKey : struct
    {
        bool DeleteElement(TResult element);
        TResult CreateElement(TResult element);
        List<TResult> GetAllElements();
        TResult GetElementDetail(TKey elementId);
        bool UpdateElement(TResult element);
    }
}
