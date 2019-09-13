using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{ [Table("ENTREPRISE")]
    public class Entreprise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntreprise { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Entreprise")]
        public string NomEntreprise { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Rue")]
        public string Rue { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Ville")]
        public string Ville { get; set; }

        [MaxLength(25), MinLength(4)]
        [Required]
        public string CodePostal { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Pays")]
        public string Pays { get; set; }


    }
}