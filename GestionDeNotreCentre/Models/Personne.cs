using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("PERSONNE")]
    public class Personne
    {
        [MaxLength(25)]
        [Required]
        public string NumeroRegistre { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nom { get; set; }

        [MaxLength(50)]
        [Required]
        public string Prenom { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(25)]
        [Required]
        public string Rue { get; set; }

        [MaxLength(25)]
        [Required]
        public string Ville { get; set; }

        [MaxLength(25)]
        [Required]
        public string CodePostal { get; set; }

        [MaxLength(25)]
        [Required]
        public string Pays { get; set; }

        [Required]
        public byte[] CV { get; set; }

        [MaxLength(25)]
        [Required]
        public string UserLogin { get; set; }

        [MaxLength(25)]
        [Required]
        public string MotDePasse { get; set; }

        public int? IdEntreprise { get; set; }

        //ajouter l'objet Entreprise pour la clef étrangère
        //public ICollection<Inscription> Inscriptions { get; set; }
        //public Employe? Employe { get; set; }
        public Formateur Formateur { get; set; }
        //public Entreprise? Entreprise { get; set; }
    }
}