using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("TACHE")]
    public class Tache
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTache { get; set; }

        [MaxLength(20)]
        [Required]
        public string LibelleTache { get; set; }

        [MaxLength(100)]
        [Required]
        public string MessageTache { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCloture { get; set; }

        [MaxLength(25)]
        [Required]
        [Display(Name ="Initiateur")]
        public string NumeroRegistreCreateur { get; set; }

        [MaxLength(25)]
        [Required]
        [Display(Name ="Destinataire")]
        public string NumeroRegistreRealisateur { get; set; }
    }
}