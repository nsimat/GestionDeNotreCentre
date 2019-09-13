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
        [MaxLength(25), MinLength(11)]
        [Required(ErrorMessage ="Le numéro de registre doit avoir au moins 11 chiffres.")]
        [Display(Name ="Numéro de registre")]
        public string NumeroRegistre { get; set; }

        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage ="Le nom de la personne doit avoir au moins 2 caractères.")]
        [Display(Name ="Nom")]
        public string Nom { get; set; }

        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage ="Le prenom de la personne doit avoir au moins 2 caractères.")]
        [Display(Name ="Prenom")]
        public string Prenom { get; set; }

        [EmailAddress]
        [MaxLength(25)]
        [Required(ErrorMessage ="Tout candidat doit fournir l'adresse de contact.")]
        [Display(Name ="Adresse électronique")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Toute personne doit signaler son numéro de contact.")]
        [MaxLength(25)]
        [Phone]
        [Display(Name ="Numéro de téléphone")]
        public string PhoneNumber { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage ="Le champ Rue ne peut être vide.")]
        [Display(Name ="Rue")]
        public string Rue { get; set; }

        [MaxLength(25), MinLength(2)]
        [Required(ErrorMessage ="Le champ Ville ne peut avoir moins de 2 caractères.")]
        [Display(Name ="Ville")]
        public string Ville { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage ="Le champ code postal est obligatoire et ne peut être vide.")]
        [Display(Name ="Code Postal")]
        public string CodePostal { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage ="")]
        [Display(Name ="Pays")]
        public string Pays { get; set; }

        [Required(ErrorMessage ="L'ajout du CV est obligatoire.")]
        [Display(Name ="CV")]
        public byte[] CV { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage ="Le champ Nom d'utilisateur ne peut être vide.")]
        [Display(Name ="Nom d'utilisateur")]
        public string UserLogin { get; set; }

        [MaxLength(25), MinLength(5)]
        [Required(ErrorMessage ="Le champ mot de passe ne peut être vide ou avoir moins de 5 caractères.")]
        [Display(Name ="Mot de passe")]
        public string MotDePasse { get; set; }

        [Display(Name ="Entreprise")]
        public int? IdEntreprise { get; set; }
        public Entreprise Entreprise { get; set; }

        //les clefs étrangères
        public ICollection<Inscription> Inscriptions { get; set; }
        public Employe Employe { get; set; }
        public Formateur Formateur { get; set; }
        
    }
}