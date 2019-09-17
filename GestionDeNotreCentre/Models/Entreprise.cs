using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{ [Table("ENTREPRISE")]
    public class Entreprise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntreprise { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Entreprise")]
        public string NomEntreprise { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Rue")]
        public string Rue { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Ville")]
        public string Ville { get; set; }

        [MaxLength(25), MinLength(4)]
        [Required]
        public string CodePostal { get; set; }

        [MaxLength(25), MinLength(3)]
        [Required]
        [Display(Name ="Pays")]
        public string Pays { get; set; }

        [Required(ErrorMessage = "Le champ 'Numéro de téléphone' ne peut être vide.")]
        [MaxLength(25)]
        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string NumeroTelephone { get; set; }

        
        public Entreprise From(IDataRecord dr)
        {
            return new Entreprise()
            {
                IdEntreprise = (int)dr["IdEntreprise"],
                NomEntreprise = (string)dr["NomEntreprise"],
                Rue = (string)dr["Rue"],
                Ville = (string)dr["Ville"],
                CodePostal = (string)dr["CodePostal"],
                Pays = (string)dr["Pays"],
                NumeroTelephone = (string)dr["NumeroTelephone"]
            };
            
        }
    }
}