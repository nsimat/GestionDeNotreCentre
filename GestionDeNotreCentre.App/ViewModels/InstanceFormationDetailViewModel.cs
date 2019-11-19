using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Extensions;
using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class InstanceFormationDetailViewModel : ViewModelBase
    {
        #region Les champs

        private readonly InstanceFormationDataService instanceFormationDataService;
        private readonly PlanificationDataService planificationDataService;
        private readonly InscriptionDataService inscriptionDataService;
        private readonly PersonneDataService personneDataService;

        private Personne supervisor;
        private InstanceFormation selectedInstanceFormation;        //L'instance de formation dont on affiche les détails
        private Inscription selectedInscription;                    //Inscription sélectionné dans la liste des inscriptions à la formation
        private Planification selectedPlanification;                //Occurence de planification concernant la formation sélectionné dans la liste des occurences de planifications
        private ObservableCollection<Inscription> inscriptions;     //Liste des demandes d'inscriptions à la formation
        private ObservableCollection<Planification> planifications; //Occurences des planifications 

        #endregion

        #region Les commandes

        public ICommand SaveInstanceFormationCommand { get; set; }
        public ICommand DeleteInstanceformationCommand { get; set; }

        public ICommand DisplaySelectedPlanificationCommand { get; set; }
        public ICommand DisplaySelectedInscriptionCommand { get; set; }

        #endregion

        #region Les propriétés

        public Personne Supervisor 
        {
            get 
            {
                return supervisor;
            }
            set 
            {
                supervisor = value;
                RaisePropertyChanged(nameof(Supervisor));
            } 
        }

        public InstanceFormation SelectedInstanceFormation
        {
            get
            {
                return selectedInstanceFormation;
            }
            set
            {
                selectedInstanceFormation = value;
                RaisePropertyChanged(nameof(SelectedInstanceFormation));
            }
        }

        public Inscription SelectedInscription
        {
            get
            {
                return selectedInscription;
            }
            set
            {
                selectedInscription = value;
                RaisePropertyChanged(nameof(SelectedInscription));
            }
        }

        public Planification SelectedPlanification
        {
            get
            {
                return selectedPlanification;
            }
            set
            {
                selectedPlanification = value;
                RaisePropertyChanged(nameof(SelectedPlanification));
            }
        }

        public ObservableCollection<Inscription> Inscriptions
        {
            get
            {
                return inscriptions;
            }
            set
            {
                inscriptions = value;
                RaisePropertyChanged(nameof(Inscriptions));
            }
        }

        public ObservableCollection<Planification> Planifications
        {
            get
            {
                return planifications;
            }
            set
            {
                planifications = value;
                RaisePropertyChanged(nameof(Planifications));
            }
        }

        #endregion

        #region Le constructeur

        public InstanceFormationDetailViewModel()
        {
            instanceFormationDataService = new InstanceFormationDataService();
            planificationDataService = new PlanificationDataService();
            inscriptionDataService = new InscriptionDataService();
            personneDataService = new PersonneDataService();

            LoadPlanifications();

            LoadInscriptions();

            LoadCommands();

            MyMessenger<EditInstanceFormationMessage>.Instance.Register(OnEditInstanceFormationMessageReceived);
        }

        #endregion

        #region Les méthodes

        private void LoadPlanifications()
        {
            Planifications = planificationDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadInscriptions()
        {
            Inscriptions = inscriptionDataService.GetAllElements().ToObservableCollection();

            foreach(Inscription inscription in inscriptions)
            {
                inscription.Personne = personneDataService.GetElementDetail(inscription.IdStatigiaire);
            }
        }

        private void LoadCommands()
        {
            SaveInstanceFormationCommand = new RelayCommand(SaveInstanceFormation, CanSaveInstanceFormation);
            DeleteInstanceformationCommand = new RelayCommand(DeleteInstanceFormation, CanDeleteInstanceFormation);

            DisplaySelectedPlanificationCommand = new RelayCommand(DisplayselectedPlanification, CanDisplaySelectedPlanification);
            DisplaySelectedInscriptionCommand = new RelayCommand(DisplaySelectedInscription, CanDisplaySelectedInscription);
        }

        private void SaveInstanceFormation(object obj)
        {
            instanceFormationDataService.UpdateElement(selectedInstanceFormation);
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());
        }

        private bool CanSaveInstanceFormation(object obj)
        {
            return true;
        }

        private void DeleteInstanceFormation(object obj)
        {
            //Effacer d'abord les clés etrangères
            if(selectedInstanceFormation.Inscriptions.Count != 0)
            {
                foreach (var inscription in selectedInstanceFormation.Inscriptions)
                {
                    inscriptionDataService.DeleteElement(inscription);
                }
            }

            if(selectedInstanceFormation.Planifications.Count != 0)
            {
                foreach(var planification in selectedInstanceFormation.Planifications)
                {
                    planificationDataService.DeleteElement(planification);
                }
            }
           
            //Effacer ensuite l'instance de formation
            instanceFormationDataService.DeleteElement(selectedInstanceFormation);            
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());            
        }

        private bool CanDeleteInstanceFormation(object obj)
        {
            return true;
        }

        private void DisplayselectedPlanification(object obj)
        {
            DisplayPlanificationFromInstanceFormationMessage message = new DisplayPlanificationFromInstanceFormationMessage { SelectedPlanification = this.SelectedPlanification };
            MyMessenger<DisplayPlanificationFromInstanceFormationMessage>.Instance.Send(message);
        }

        private bool CanDisplaySelectedPlanification(object obj)
        {
            if (SelectedPlanification != null)
                return true;
            return false;
        }

        private void DisplaySelectedInscription(object obj)
        {
            DisplayInscriptionFromInstanceFormationMessage message = new DisplayInscriptionFromInstanceFormationMessage { SelectedInscription = this.SelectedInscription };
            MyMessenger<DisplayInscriptionFromInstanceFormationMessage>.Instance.Send(message);
        }

        private bool CanDisplaySelectedInscription(object obj)
        {
            if (SelectedInscription != null)
                return true;
            return false;
        }

        private void OnEditInstanceFormationMessageReceived(EditInstanceFormationMessage obj)
        {
            SelectedInstanceFormation = obj.SelectedInstanceFormation;
            supervisor = personneDataService.GetElementDetail(selectedInstanceFormation.IdEmploye);
        }

        #endregion
    }
}
