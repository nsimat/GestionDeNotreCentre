using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataAccess
{
    public class Command
    {
        private IDictionary<string, object> _Parameters;
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }

        public object this[string parameterName]
        {
            set { Parameters[parameterName] = value; }
        }

        public void AddParameter (string ParameterName , object Value)
        {
            Parameters.Add(ParameterName, Value);
        }
        internal IDictionary<string, object> Parameters
        {
            get { return _Parameters ?? (_Parameters = new Dictionary<string, object>()); }
        }

        public Command(string query) : this(query, false)
        {
        }

        public Command(string query, bool isStoredProcedure)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException();

            Query = query;
            IsStoredProcedure = isStoredProcedure;
        }
    }
}
