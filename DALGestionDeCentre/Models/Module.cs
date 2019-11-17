using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("MODULE")]
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModule { get; set; }

        [MinLength(3)]
        [Required(ErrorMessage ="Le nom de module doit avoir au moins 3 caractères.")]
        [Display(Name ="Nom")]
        public string Nom { get; set; }

        [MinLength(3)]
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

        public Module From(IDataRecord dr)
        {
            return new Module()
            {
                IdModule = (int)dr["IdModule"],
                Nom = (string)dr["Nom"],
                DescriptionModule = (string)dr["DescriptionModule"],
                TableDeMatieres = (byte[])dr["TableDeMatieres"],
                PrixJournalier = (decimal)dr["PrixJournalier"],
                NombreJours = (int)dr["NombreJours"],
                NbreJoursAffectes = (int)dr["NbreJoursAffectes"],

            };
        }
    }
}