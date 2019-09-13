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

        [MaxLength(50)]
        [Required]
        public string Nom { get; set; }

        [MaxLength(50)]
        [Required]
        public string DescriptionModule { get; set; }

        [Required]
        public byte[] TableDeMatieres { get; set; }

        [Required]
        public decimal PrixJournalier { get; set; }

        [Required]
        public int NombreJours { get; set; }

        [Required]
        public int NbreJoursAffectes { get; set; }

        public ICollection<PreRequis> PreRequis { get; set; }
        public ICollection<Planification> Planifications { get; set; }
        //public ICollection<Compentence> Competences { get; set; }
        //public ICollection<Composition> Compositions { get; set; }
    }
}