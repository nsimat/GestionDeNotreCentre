using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("TCHAT")]
    public class Tchat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTchat { get; set; }

        [MaxLength(100), MinLength(3)]
        [Required(ErrorMessage ="Un message doit avoir au moins 3 caractères.")]
        [Display(Name ="Message")]
        public string MessageTchat { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de début")]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de clôture")]
        public DateTime? DateCloture { get; set; }

        [Required(ErrorMessage ="Le champ 'Envoyeur' ne peut être vide.")]
        [MaxLength(25)]
        [Display(Name ="Envoyeur")]
        public string NumeroRegistreEnvoyeur { get; set; }

        [Required(ErrorMessage ="Le champ 'Recepteur' ne peut être vide.")]
        [MaxLength(25)]
        [Display(Name ="Recepteur")]
        public string NumeroRegistreRecepteur { get; set; }
    }
}