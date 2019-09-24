using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("MODULE")]
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModule { get; set; }

        [MaxLength(50), MinLength(3)]
        [Required(ErrorMessage ="Le nom de module doit avoir au moins 3 caractères.")]
        [Display(Name ="Nom")]
        public string Nom { get; set; }

        [MaxLength(50), MinLength(3)]
        [Required(ErrorMessage ="Une description doit avoir au moins 3 caractères.")]
        [Display(Name ="Description")]
        public string DescriptionModule { get; set; }

        [Required]
        [Display(Name ="Table de matières")]
        public byte[] TableDeMatieres { get; set; }

        [Required]
        [Display(Name ="Prix Journalier")]
        public decimal PrixJournalier { get; set; }

        [Required]
        [Display(Name ="Nombre de jours")]
        public int NombreJours { get; set; }

        [Required]
        [Display(Name ="Nombre de jours affectés")]
        public int NbreJoursAffectes { get; set; }

        public ICollection<PreRequis> PreRequis { get; set; }
        public ICollection<Planification> Planifications { get; set; }
        public ICollection<Competence> Competences { get; set; }
        public ICollection<Composition> Compositions { get; set; }
    }
}