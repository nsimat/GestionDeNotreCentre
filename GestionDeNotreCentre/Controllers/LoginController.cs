using DALGestionDeCentre.Services;
using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using GestionDeNotreCentre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToolBox.Repositories;

namespace GestionDeNotreCentre.Controllers
{
    public class LoginController : Controller
    {
        private readonly PersonneRepository persoRepo = new PersonneRepository();

        // GET: Login
        public ActionResult Index()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            Personne personne = new Personne();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                personne = persoRepo.Get(HttpContext.User.Identity.Name);//à modifier pour récupérer la personne à partir de son username (unique) et le mapper avec le viewModel
            }

            if (personne != null)
            {
                viewModel.Email = personne.UserLogin;
                viewModel.MotDePasse = personne.MotDePasse;

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Personne personne = persoRepo.Authentifier(loginViewModel.Email, loginViewModel.MotDePasse);

                if (personne != null)
                {
                    FormsAuthentication.SetAuthCookie(personne.IdPersonne.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");//à revoir
                }
                ModelState.AddModelError(loginViewModel.Email, "Email et/ou mot de passe incorrect(s)!");

            }

            return View(loginViewModel);
        }

        public ActionResult Deconnexion()//Logout à faire
        {

            return View("/");//à revoir
        }


    }
}