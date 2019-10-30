using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Messages
{
    public class PersonneAuthenticatedMessage
    {
        public Personne Employe { get; set; }
        public bool Authenticated { get; set; }
    }
}
