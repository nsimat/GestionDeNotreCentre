using DALGestionDeCentre.Repositories;
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
    public class InstanceFormationController : Controller
    {
        private readonly InstanceFormationRepository instanceRepo = new InstanceFormationRepository();  
        private readonly FormationRepository formationRepo = new FormationRepository();
        private readonly EmployeRepository employeRepo = new EmployeRepository();
        private readonly InscriptionRepository inscripRepo = new InscriptionRepository();
        private readonly PersonneRepository personneRepo = new PersonneRepository();

        // GET: InstanceFormation
        public ActionResult Index()
        {
            return View("AfficherFormations");
        }
        
        public ActionResult AfficherFormations()
        {
            //Ajout pour utiliser ajax et DataTables
            var instanceFormations = instanceRepo.Get().ToList();
            //var size = instanceFormations.Count();
            

            //for (int i = 0; i < size; i++)
            foreach(var instanceFormation in instanceFormations)
            {
                //Formation formation = new Formation();
                //formation = formationRepo.Get(instanceFormations.ElementAt(i).IdFormation);
                //instanceFormations.ElementAt(i).Formation = formationRepo.Get(instanceFormations.ElementAt(i).IdFormation);
                instanceFormation.Formation = formationRepo.Get(instanceFormation.IdFormation);
            }

            var jsonResult = Json(new { data = instanceFormations }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: InstanceFormation/Details/5        
        public ActionResult Details(int id)
        {
            //Créer une vue model qui reprend Personne, InstanceFormation, Session, Formation, Composition            

            var instanceFormation = instanceRepo.Get(id);
            instanceFormation.Formation = formationRepo.Get(instanceFormation.IdFormation);

            return View("DetailsInstanceFormation", instanceFormation);
        }

        [Authorize]
        public ActionResult InscrirePersonne(int id)
        {            
            InstanceFormation instanceFormation = instanceRepo.Get(id);
            instanceFormation.Employe = employeRepo.Get(instanceFormation.IdEmploye);
            instanceFormation.Employe.Personne = employeRepo.GetEmployes().FirstOrDefault(p => p.IdPersonne == instanceFormation.IdEmploye);
            instanceFormation.Formation = formationRepo.Get(instanceFormation.IdFormation);

            InstanceFormationViewModel viewModel = new InstanceFormationViewModel()
            {
                InstanceFormation = instanceFormation,
                Formation = instanceFormation.Formation,
                //Personne = personneRepo.Get((int)Session["UserId"])
                Personne = personneRepo.Get(HttpContext.User.Identity.Name)//ajout pour voir si ça fonctionne
            };
            ViewBag.InscriptionData = viewModel;
            

            return View("InscriptionAFormation", viewModel);//InscrirePersonne
        }


        //[HttpPost]
        //public ActionResult InscrirePersonne()
        //{
        //    Inscription inscriptionCreated;
        //    InstanceFormationViewModel model = (InstanceFormationViewModel)TempData["InscriptionData"];

        //    Inscription inscription = new Inscription()
        //    {
        //        DateInscription = DateTime.Now,
        //        IdInstanceFormation = model.InstanceFormation.IdInstanceFormation,
        //        IdStatigiaire = model.Personne.IdPersonne,
        //        IdEmploye = model.InstanceFormation.IdPersonne
        //    };

        //    inscriptionCreated = inscripRepo.Insert(inscription);

        //    return RedirectToAction("AfficherNewInscription", "Inscription",inscriptionCreated.IdInscription);
        //}
    }
}