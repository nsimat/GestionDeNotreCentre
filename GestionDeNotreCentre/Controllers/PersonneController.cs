using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using GestionDeNotreCentre.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolBox.Repositories;

namespace GestionDeNotreCentre.Controllers
{
    public class PersonneController : Controller
    {
        private readonly PersonneRepository persoRepo = new PersonneRepository();
        // GET: Personne
        public ActionResult Index()
        {
            return View(persoRepo.Get());
        }

        // GET: Personne/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /*Ajout de l'Action NewPersonne*/
        // GET: Personne/NewPersonne
        public ActionResult NewPersonne()
        {
            var personne = new PersonneViewModel();
            return View(personne);
        }

        [HttpPost]
        public ActionResult NewPersonne(PersonneViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            String FileExt = Path.GetExtension(viewModel.AttachedFile.FileName).ToUpper();

            if (FileExt == ".PDF")
            {

                byte[] uploadedFile = new byte[viewModel.AttachedFile.InputStream.Length];
                viewModel.AttachedFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                //Mappage avec la classe Personne
                Personne utilisateur = new Personne
                {
                    NumeroRegistre = viewModel.NumeroRegistre,
                    Nom = viewModel.Nom,
                    Prenom = viewModel.Prenom,
                    Email = viewModel.Email,
                    Rue = viewModel.Rue,
                    Ville = viewModel.Ville,
                    CodePostal = viewModel.CodePostal,
                    Pays = viewModel.Pays,
                    NumeroTelephone = viewModel.NumeroTelephone,
                    UserLogin = viewModel.UserLogin,
                    MotDePasse = HashPassword.Hash(viewModel.MotDePasse),
                    PersonCV = uploadedFile
                };

                
                //utilisateur.Entreprise = personne.NomEntreprise;//à revoir car erreur

                persoRepo.Insert(utilisateur);

            }
            else
            {
                ViewBag.Status = "Format de fichier invalide.";
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }






        // GET: Personne/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Personne/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personne/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Personne/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
