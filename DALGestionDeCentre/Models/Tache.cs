using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("TACHE")]
    public class Tache
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTache { get; set; }

        [MaxLength(20), MinLength(2)]
        [Display(Name ="Libellé")]
        [Required(ErrorMessage ="Le champ 'Libellé' ne peut être vide et doit avoir au moins 2 caractères.")]
        public string LibelleTache { get; set; }

        [MaxLength(100), MinLength(2)]
        [Required(ErrorMessage ="Un message doit avoir au moins 2 caractères.")]
        [Display(Name ="Message")]
        public string MessageTache { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de création")]
        public DateTime DateCreation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de clôture")]
        public DateTime? DateCloture { get; set; }

        [MaxLength(25)]
        [Required]
        [Display(Name ="Initiateur")]
        public string NumeroRegistreCreateur { get; set; }
        public Employe Createur { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage ="Tout meesage doit avoir un destinataire.")]
        [Display(Name ="Destinataire")]
        public string NumeroRegistreRealisateur { get; set; }
        public Employe Realisateur { get; set; }

        public Tache From(IDataRecord dr)
        {
            return new Tache()
            {
                IdTache = (int)dr["IdTache"],
                LibelleTache = (string)dr["LibelleTache"],
                MessageTache = (string)dr["MessageTache"],
                DateCreation = (DateTime)dr["DateCreation"],
                DateCloture = (DateTime)dr["DateCloture"],
                /* Createur     = (string)dr["IdCreateur"],
                 Realisateur = (string)dr["IdRealisateur"]*/

            };

        }
    }
}