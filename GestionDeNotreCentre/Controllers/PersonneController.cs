using GestionDeNotreCentre.Models;
using GestionDeNotreCentre.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create()
        {            
            return View();
        }        

        [HttpPost]
        public ActionResult Create(Personne personne)
        {
            String FileExt = Path.GetExtension(personne.AttachedFile.FileName).ToUpper();

            if(FileExt == ".PDF")
            {
                //Stream stream = personneCV.InputStream;
                //BinaryReader reader = new BinaryReader(stream);
                //byte[] fileContent = reader.ReadBytes((Int32)stream.Length);

                byte[] uploadedFile = new byte[personne.AttachedFile.InputStream.Length];
                personne.AttachedFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                personne.personCV = uploadedFile;

                persoRepo.Insert(personne);

            }
            else
            {
                ViewBag.Status = "Format de fichier invalide.";
                return View();
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
