using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("FORMATION")]
    public class Formation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormation { get; set; }

        [StringLength(50, ErrorMessage = "Le nom de la formation doit avoir une longueur d'au plus 50 caractères.")]
        [Required]
        [Display(Name ="Formation")]
        public string Nom { get; set; }

        public ICollection<Composition> Compositions { get; set; }

    }
}