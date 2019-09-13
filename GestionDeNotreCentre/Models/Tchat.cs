using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("TCHAT")]
    public class Tchat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTchat { get; set; }

        [MaxLength(100)]
        [Required]
        public string MessageTchat { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCloture { get; set; }

        [Required]
        [Display(Name ="Envoyeur")]
        public string NumeroRegistreEnvoyeur { get; set; }

        [Required]
        [Display(Name ="Recepteur")]
        public string NumeroRegistreRecepteur { get; set; }
    }
}