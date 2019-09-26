using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GestionDeNotreCentre.Models
{
    public class PersonneViewModel
    {
        [MaxLength(25), MinLength(11)]
        [Required(ErrorMessage = "Le numéro de registre doit avoir au moins 11 chiffres.")]
        [Display(Name = "Numéro de registre")]
        public string NumeroRegistre { get; set; }

        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage = "Le champ 'Nom' doit contenir au moins 2 caractères.")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage = "Le champ 'Prenom' doit contenir au moins 2 caractères.")]
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }

        [EmailAddress]
        [MaxLength(25)]
        [Required(ErrorMessage = "Veuillez fournir votre adresse de contact.")]
        [Display(Name = "Adresse électronique")]
        public string Email { get; set; }

        [EmailAddress]
        [MaxLength(25)]
        [Required(ErrorMessage = "Les champs 'Confirmer adresse électronique' et 'Adresse éléctronique' ne correspondent pas.")]
        [Display(Name = "Confirmer adresse électronique")]
        [Compare("Email")]
        public string ConfirmationEmail { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "Le champ 'Rue' ne peut être vide.")]
        [Display(Name = "Rue")]
        public string Rue { get; set; }

        [MaxLength(25), MinLength(2)]
        [Required(ErrorMessage = "Le champ 'Ville' ne peut avoir moins de 2 caractères.")]
        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "Le champ 'Code Postal' est obligatoire et ne peut être vide.")]
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "Le champ 'Pays' ne peut être vide ou doit avoir moins de 25 caractères.")]
        [Display(Name = "Pays")]
        public string Pays { get; set; }

        [Required(ErrorMessage = "Le champ 'Numéro de téléphone' ne peut être vide.")]
        [MaxLength(25)]
        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string NumeroTelephone { get; set; }

        [Required(ErrorMessage = "L'ajout du CV est obligatoire.")]
        [DataType(DataType.Upload)]
        [Display(Name = "Attacher votre CV au format PDF")]
        public HttpPostedFileBase AttachedFile { get; set; }        

        [EmailAddress]
        [MaxLength(25)]
        [Required(ErrorMessage = "Le champ 'Nom d'utilisateur' est l'adresse éléctronique fournie par l'utilisateur.")]
        [Display(Name = "Nom d'utilisateur")]
        [Compare("Email")]
        public string UserLogin { get; set; }          

        [MaxLength(25), MinLength(5)]
        [Required(ErrorMessage = "Le champ 'Mot de passe' ne peut être vide ou avoir moins de 5 caractères.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [MaxLength(25), MinLength(5)]
        [Required(ErrorMessage = "Les champs 'Mot de passe' et 'Confirmer Mot de passe' ne correspondent pas.")]
        [Display(Name = "Confirmer Mot de passe")]
        [DataType(DataType.Password)]
        [Compare("MotDePasse")]
        public string ConfirmMotDePasse { get; set; }

        [Display(Name = "Entreprise")]
        public string NomEntreprise { get; set; }

        public bool Authentifie { get; set; }//propriété ajoutée pour voir si une personne est authentifiée
    }
}