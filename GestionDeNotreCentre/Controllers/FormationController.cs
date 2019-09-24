using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeNotreCentre.Controllers
{
    public class FormationController : Controller
    {
        private readonly FormationRepository formatRepo = new FormationRepository();
        // GET: Formation
        public ActionResult Index()
        {
            return View(formatRepo.Get());
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Formation formation)
        {

            formatRepo.Insert(formation);
            
            return RedirectToAction("Index");
            
        }

        
        public ActionResult Edit(int id)
        {
            return View(formatRepo.Get(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //[HttpPost]
        //public ActionResult Edit(Formation formation)
        //{
        //    if (formatRepo.Update(formation, formation.IdFormation))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return null;
        //}

        public ActionResult Delete(int id)
        {
            return View(formatRepo.Get(id));
        }

        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}