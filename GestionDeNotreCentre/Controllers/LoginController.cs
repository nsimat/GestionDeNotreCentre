using GestionDeCentreDAL.Services;
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
using Microsoft.AspNet.Identity;

namespace GestionDeNotreCentre.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly PersonneRepository persoRepo = new PersonneRepository();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()//string returnUrl
        {            

            LoginViewModel viewModel = new LoginViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            Personne personne = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                personne = persoRepo.Get(HttpContext.User.Identity.Name);
            }

            if (personne != null)
            {
                viewModel.Email = personne.UserLogin;
                viewModel.MotDePasse = personne.MotDePasse;

                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Personne personne = persoRepo.Authentifier(loginViewModel.Email, loginViewModel.MotDePasse);
                                

                if (personne != null)
                {
                    loginViewModel.Authentifie = true;
                    HttpContext.Session["UserName"] = personne.Prenom + " " + personne.Nom;
                    HttpContext.Session["UserId"] = personne.IdPersonne;
                    ViewBag.IdentifiedPersonne = personne;
                    FormsAuthentication.SetAuthCookie(personne.IdPersonne.ToString(), loginViewModel.RememberMe);//false est remplacé par RememberMe???
                    
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("AfterLogin");//Mettre ici l'accès vers la vue AfterLogin à modifier
                }
                ModelState.AddModelError(loginViewModel.Email, "Email et/ou mot de passe incorrect(s)!");

            }

            return View(loginViewModel);
        }

        [Authorize]
        public ActionResult AfterLogin()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();//ajout -- revoir si ça marche pour ce cas
            return RedirectToAction("Index", "Home");//Retourne vers le home, la page d'accueil à modifier?
        }


    }
}