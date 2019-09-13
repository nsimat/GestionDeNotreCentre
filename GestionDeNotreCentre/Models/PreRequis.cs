using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("PREREQUIS")]
    public class PreRequis
    {        
        public int IdModulePreRequis { get; set; }
        public Module ModulePreRequis { get; set; }


        public int IdModule { get; set; }
        public Module Module { get; set; }
    }
}