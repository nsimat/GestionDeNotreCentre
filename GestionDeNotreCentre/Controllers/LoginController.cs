using GestionDeNotreCentre.Models;
using GestionDeNotreCentre.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeNotreCentre.Controllers
{
    public class LoginController : Controller
    {
        private readonly PersonneRepository persoRepo = new PersonneRepository();

        // GET: Login
        public ActionResult Index()
        {
            PersonneViewModel viewModel = new PersonneViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            Personne personne;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                personne = persoRepo.Get(1);//à modifier pour récupérer la personne à partir de son username (unique) et le mapper avec le viewModel
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {            
            var personne = persoRepo.Authentifier(loginViewModel.Email, loginViewModel.MotDePasse);

            if (personne != null)
            {
                return RedirectToAction("");
            }
            
            return View();
            
        }     

        
    }
}