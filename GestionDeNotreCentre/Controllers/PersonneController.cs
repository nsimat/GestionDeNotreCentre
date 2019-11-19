using GestionDeCentreDAL.Services;
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
using System.Web.Security;

namespace GestionDeNotreCentre.Controllers
{
    public class PersonneController : Controller
    {
        private readonly PersonneRepository persoRepo = new PersonneRepository();
        // GET: Personne
        public ActionResult Index()
        {            
            return View();            
        }

        public ActionResult GetPersonnes()
        {
            //Ajout pour utiliser ajax et DataTables
            var personnes = persoRepo.Get().ToList();
            var jsonResult = Json(new { data = personnes }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: Personne/Details/5
        public ActionResult Details(int id)
        {
            return View("DetailsPersonne", persoRepo.Get(id));
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
            Personne personneInserted;

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
                    UserLogin = viewModel.Email,
                    MotDePasse = HashPassword.Hash(viewModel.MotDePasse),
                    PersonCV = uploadedFile
                };                
                

                Personne personne = persoRepo.Insert(utilisateur);
                personneInserted =  persoRepo.GetPersonneFrom(personne.UserLogin);
                
                FormsAuthentication.SetAuthCookie(personneInserted.IdPersonne.ToString(), false);//Ajout pour créer le cookie de la personne insérée

            }
            else
            {
                ViewBag.Status = "Format de fichier invalide.";
                return View(viewModel);
            }

            return RedirectToAction("Details", new { id = personneInserted.IdPersonne });
        }


        public FileResult Telecharger(int id)
        {
            Personne personne = persoRepo.Get(id);
            string nomPersonne = personne.Prenom + " " + personne.Nom + ".pdf";
            return File(personne.PersonCV, "application/pdf", "CV de " + nomPersonne);
        }

        // GET: Personne/Edit/5
        public ActionResult Edit(int id)
        {
            return View(persoRepo.Get(id));
        }

        // POST: Personne/Edit/5
        [HttpPost]
        public ActionResult Edit(Personne personne)
        {
            if(persoRepo.Put(personne, personne.IdPersonne))
            {
                return RedirectToAction("Index");//à revoir
            }
            return null;
        }

        // GET: Personne/Delete/5
        public ActionResult Delete(int id)
        {
            return View("DeletePersonne", persoRepo.Get(id));
        }

        // POST: Personne/Delete/5
        [HttpPost]
        public ActionResult Delete(Personne personne)
        {
            if (persoRepo.Delete(personne.IdPersonne))
            {
                return RedirectToAction("Index");
            }
            return null;
        }
    }
}
