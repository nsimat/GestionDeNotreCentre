using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeNotreCentre.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly EntrepriseRepository entrepRepo = new EntrepriseRepository();
        // GET: Entreprise
        public ActionResult Index()
        {
            return View(entrepRepo.Get());
        }

        // GET: Entreprise/Details/5
        public ActionResult Details(int id)
        {
            return View(entrepRepo.Get(id));
        }

        // GET: Entreprise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entreprise/Create
        [HttpPost]
        public ActionResult Create(Entreprise entreprise)
        {
            
            entrepRepo.Insert(entreprise);
           return RedirectToAction("Index");
           
        }



        // GET: Entreprise/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View(entrepRepo.Get(id));
        }

        // POST: Entreprise/Edit/5
        [HttpPost]
        public ActionResult Edit(Entreprise entrep)
        {
            if (entrepRepo.Put(entrep, entrep.IdEntreprise))
            {
                return RedirectToAction("Index");
            }
            return null;
        }

        // GET: Entreprise/Delete/5
        
        public ActionResult Delete(int id)
        {
             return View(entrepRepo.Get(id));
            //return entrepRepo.Delete(id);
        }

        // POST: Entreprise/Delete/5
        [HttpPost]
        public ActionResult Delete(Entreprise entrep)
        {
            if (entrepRepo.Delete(entrep.IdEntreprise))
            {
                return RedirectToAction("Index");
            }
            return null;
        }
    }
}
