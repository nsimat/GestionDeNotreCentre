using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataAccess
{
    public interface IConnection
    {
        object ExecuteScalar(Command command);

        int ExecuteNonQuery(Command command);


        IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector);
        
    }
}
