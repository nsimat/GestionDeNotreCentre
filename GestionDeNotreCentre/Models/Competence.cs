using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("COMPETENCE")]
    public class Competence
    {
        public string NumeroRegistre { get; set; }

        public int IdModule { get; set; }
    }
}