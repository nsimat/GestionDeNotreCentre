using GestionDeNotreCentre.Models;
using GestionDeNotreCentre.Repositories;
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

        // GET: Personne/Create
        //public ActionResult Create()
        //{            
        //    return View();
        //}        

        //[HttpPost]
        //public ActionResult Create(Personne personne)
        //{
        //    String FileExt = Path.GetExtension(personne.AttachedFile.FileName).ToUpper();

        //    if(FileExt == ".PDF")
        //    {
        //        //Stream stream = personneCV.InputStream;
        //        //BinaryReader reader = new BinaryReader(stream);
        //        //byte[] fileContent = reader.ReadBytes((Int32)stream.Length);

        //        byte[] uploadedFile = new byte[personne.AttachedFile.InputStream.Length];
        //        personne.AttachedFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

        //        personne.personCV = uploadedFile;

        //        persoRepo.Insert(personne);

        //    }
        //    else
        //    {
        //        ViewBag.Status = "Format de fichier invalide.";
        //        return View();
        //    }

        //    return RedirectToAction("Index");
        //}  

        // GET: Personne/NewPersonne
        public ActionResult NewPersonne()
        {
            var perso = new PersonneViewModel();
            return View();
        }

        [HttpPost]
        public ActionResult NewPersonne(PersonneViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            String FileExt = Path.GetExtension(model.AttachedFile.FileName).ToUpper();

            if (FileExt == ".PDF")
            {            

                byte[] uploadedFile = new byte[model.AttachedFile.InputStream.Length];
                model.AttachedFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                //Mappage avec la classe Personne
                Personne utilisateur = new Personne();

                utilisateur.NumeroRegistre = model.NumeroRegistre;
                utilisateur.Nom = model.Nom;
                utilisateur.Prenom = model.Prenom;
                utilisateur.Email = model.Email;
                utilisateur.Rue = model.Rue;
                utilisateur.Ville = model.Ville;
                utilisateur.CodePostal = model.CodePostal;
                utilisateur.Pays = model.Pays;
                utilisateur.NumeroTelephone = model.NumeroTelephone;
                utilisateur.UserLogin = model.UserLogin;

                string motDePasseEncode = HashPassword.Hash(model.MotDePasse);
                utilisateur.MotDePasse = motDePasseEncode;
                utilisateur.personCV = uploadedFile;
                //utilisateur.Entreprise = personne.NomEntreprise;//à revoir car erreur

                persoRepo.Insert(utilisateur);

            }
            else
            {
                ViewBag.Status = "Format de fichier invalide.";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///    Cette action permet de vérifier le login d'une personne qui souhaite s"isncrire à une formation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Login(string username, string password)
        {
            string motDePasseEncode = HashPassword.Hash(password);
            var personne = persoRepo.Get(password);//à modifier
            return View();
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

        
    }
}
