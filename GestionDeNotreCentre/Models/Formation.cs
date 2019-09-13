using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("FORMATION")]
    public class Formation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormation { get; set; }

        [StringLength(50, ErrorMessage = "Le nom doit avoir une longueur d'au plus 50 caractères.")]
        [Required]
        public string Nom { get; set; }

    }
}