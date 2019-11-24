using GestionDeCentreDAL.Models;
using GestionDeCentreDAL.Repositories;
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
        private readonly InstanceFormationRepository instanceFormationRepository = new InstanceFormationRepository();
        private readonly PersonneRepository personneRepository = new PersonneRepository();
        private readonly EmployeRepository employeRepository = new EmployeRepository();
        private readonly FormationRepository formationRepository = new FormationRepository();

        // GET: Inscription        
        public ActionResult Index()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        public ActionResult AfficherNewInscription(int id)
        {
            Inscription inscription = inscriptionRepo.Get(id);

            inscription.Employe = employeRepository.Get(inscription.IdEmploye);
            inscription.Employe.Personne = personneRepository.Get(inscription.IdEmploye);
            inscription.Personne = personneRepository.Get(inscription.IdStatigiaire);

            inscription.InstanceFormation = instanceFormationRepository.Get(inscription.IdInstanceFormation);
            inscription.InstanceFormation.Formation = formationRepository.Get(inscription.InstanceFormation.IdFormation);

            return View(inscription);
        }

        [HttpPost] 
        //[ValidateAntiForgeryToken]
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
            Inscription newInscription = inscriptionRepo.GetInscriptionFrom(inscription);

            return RedirectToAction("AfficherNewInscription", new { id = newInscription.IdInscription });
        }
    }
}