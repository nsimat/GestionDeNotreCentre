using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("FORMATEUR")]
    public class Formateur
    {
        [Required]
        [Display(Name ="Formateur")]
        public string NumeroRegistre { get; set; }

        public ICollection<Planification> Planifications { get; set; }
        public ICollection<Competence> Competences { get; set; }
    }
}