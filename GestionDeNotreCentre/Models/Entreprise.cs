using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{ [Table("ENTREPRISE")]
    public class Entreprise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntreprise { get; set; }

        [MaxLength(50)]
        [Required]
        public string NomEntreprise { get; set; }

        [MaxLength(25)]
        [Required]
        public string Rue { get; set; }

        [MaxLength(25)]
        [Required]
        public string Ville { get; set; }

        [MaxLength(25)]
        [Required]
        public string CodePostal { get; set; }

        [MaxLength(25)]
        [Required]
        public string Pays { get; set; }


    }
}