using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    public class LoginViewModel
    {
        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage = "Le champ 'Adresse électronique' doit contenir une adresse électronique valide.")]
        [Display(Name = "Adresse électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50), MinLength(2)]
        [Required(ErrorMessage = "Le champ 'Mot de passe' doit contenir entre 2 et 50 caractères.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [Display(Name = "Restez connecté(e)?")]
        public bool RememberMe { get; set; }
    }
}