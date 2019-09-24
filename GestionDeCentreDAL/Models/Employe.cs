using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Personne Personne { get; set; }

    }
}