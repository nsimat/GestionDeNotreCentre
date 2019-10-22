﻿using DALGestionDeCentre.Repositories;
using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeNotreCentre.Controllers
{
    [Authorize]
    public class InscriptionController : Controller
    {
        private readonly InscriptionRepository inscriptionRepo = new InscriptionRepository();

        // GET: Inscription        
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult AfficherNewInscription(int id)
        {
            Inscription inscription = inscriptionRepo.Get(id);

            return View(inscription);
        }

        [HttpPost]        
        public ActionResult ConfirmerInscription()//InstanceFormationViewModel model
        {
            Inscription inscriptionCreated;
            InstanceFormationViewModel model = (InstanceFormationViewModel)TempData["InscriptionData"];
            
            Inscription inscription = new Inscription()
            {
                DateInscription = DateTime.Now,
                IdInstanceFormation = model.InstanceFormation.IdInstanceFormation,
                IdStatigiaire = model.Personne.IdPersonne,
                IdEmploye = model.InstanceFormation.IdEmploye
            };

            inscriptionCreated = inscriptionRepo.Insert(inscription);

            return RedirectToAction("AfficherNewInscription", new { id = inscriptionCreated.IdInscription });
        }
    }
}