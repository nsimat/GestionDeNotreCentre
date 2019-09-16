using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Mapper
{
    /// <summary>
    /// Permet de faire le mapping de n'importe quel type d'objet
    /// </summary>
    /// <typeparam name="T">Type d'objet a mapper</typeparam>
    /// <typeparam name="U">Type d'objet de retour </typeparam>
    public static class Mapper<T, U>
         where U : class, new()
    {
        /// <summary>
        /// Retourne un objet du type de retour 
        /// </summary>
        /// <param name="obj">Objet a mapper</param>
        /// <returns>Objet tranformer au type voulu.</returns>
        public static U Map(T obj)
        {
            U retour = (U)Activator.CreateInstance(typeof(U));
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                if (typeof(U).GetProperty(item.Name) != null)
                {
                    typeof(U).GetProperty(item.Name).SetValue(retour, item.GetValue(obj));
                }
            }
            return retour;
        }
    }
}


