using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    
   [Table("EMPLOYE")]
    public class Employe
    {
        [Required]
        [Display(Name = "Employe")]
        public string NumeroRegistre { get; set; }

       
    }
}