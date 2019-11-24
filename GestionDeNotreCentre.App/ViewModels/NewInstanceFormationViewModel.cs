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
    public class NewInstanceFormationViewModel : ViewModelBase
    {
        #region Les champs

        private readonly InstanceFormationDataService instanceFormationDataService;
        private readonly FormationDataService formationDataService;
        private readonly EmployeDataService employeDataService;
        private readonly CompositionDataService compositionDataService;
        private readonly ModuleDataService moduleDataService;

        private InstanceFormation newInstanceFormation; //L'instance de formation que l'on veut créer  
        private ObservableCollection<Formation> formations;//
        private Formation selectedFormation;
        private string selectedStatut;        
        private DateTime selectedDateDebut, selectedDateFin;
        private ObservableCollection<string> formationStatuts;
        private Personne responsable;

        #endregion

        #region Les commandes

        public ICommand SaveNewInstanceFormationCommand { get; set; }
        public ICommand CancelNewInstanceFormationCommand { get; set; }

        #endregion

        #region Les propriétés

        public List<string> Statuts { get; set; }        

        public string SelectedStatut
        {
            get
            {
                return selectedStatut;
            }
            set
            {
                selectedStatut = value;
                RaisePropertyChanged(nameof(SelectedStatut));
            }
        }

        public InstanceFormation NewInstanceFormation
        {
            get
            {
                return newInstanceFormation;
            }
            set
            {
                newInstanceFormation = value;
                RaisePropertyChanged(nameof(NewInstanceFormation));
            }
        }

        public Formation SelectedFormation
        {
            get
            {
                return selectedFormation;
            }
            set
            {
                selectedFormation = value;
                RaisePropertyChanged(nameof(SelectedFormation));
            }
        }

        public DateTime SelectedDateDebut
        {
            get
            {
                return selectedDateDebut;
            }
            set
            {
                selectedDateDebut = value;
                RaisePropertyChanged(nameof(SelectedDateDebut));
            }
        }

        public DateTime SelectedDateFin
        {
            get
            {
                return selectedDateFin;
            }
            set
            {
                selectedDateFin = value;
                RaisePropertyChanged(nameof(SelectedDateFin));
            }
        }

        public Personne Responsable
        {
            get
            {
                return responsable;
            }
            set
            {
                responsable = value;
                RaisePropertyChanged(nameof(Responsable));
            }
        }

        public ObservableCollection<Formation> Formations
        {
            get
            {
                return formations;
            }
            set
            {
                formations = value;
                RaisePropertyChanged(nameof(Formations));
            }
        }
        
        public ObservableCollection<string> FormationStatuts
        {
            get
            {
                return formationStatuts;
            }
            set
            {
                formationStatuts = value;
                RaisePropertyChanged(nameof(FormationStatuts));
            }
        }

        #endregion

        #region Le constructeur

        public NewInstanceFormationViewModel()
        {
            newInstanceFormation = new InstanceFormation();
            instanceFormationDataService = new InstanceFormationDataService();
            formationDataService = new FormationDataService();
            employeDataService = new EmployeDataService();
            compositionDataService = new CompositionDataService();
            moduleDataService = new ModuleDataService();

            Statuts = new List<string>
            {
                "in preparation",
                "started",
                "canceled",
                "completed"
            };

            FormationStatuts = Statuts.ToObservableCollection();

            InitDatesAndFormations();

            LoadCommands();

            MyMessenger<PersonneAuthenticatedMessage>.Instance.Register(OnPersonneAuthenticatedMessageReceived);
        }

        #endregion

        #region Les méthodes

        private void OnPersonneAuthenticatedMessageReceived(PersonneAuthenticatedMessage obj)
        {
            Responsable = obj.Employe;
        }

        private void InitDatesAndFormations()
        {
            SelectedDateDebut = DateTime.Today;
            SelectedDateFin = DateTime.Today;
            Formations = formationDataService.GetAllElements().ToObservableCollection();

            foreach(var formation in formations)
            {
                formationDataService.EstimationOfDays(formation);
            }

        }

        private void LoadCommands()
        {
            SaveNewInstanceFormationCommand = new RelayCommand(SaveNewInstanceFormation, CanSaveNewInstanceFormation);
            CancelNewInstanceFormationCommand = new RelayCommand(CancelNewInstanceFormation, CanCancelNewInstanceFormation);
        }

        private void SaveNewInstanceFormation(object obj)
        {
            newInstanceFormation.IdEmploye = responsable.IdPersonne;
            newInstanceFormation.Statut = selectedStatut;
            newInstanceFormation.DateDebut = selectedDateDebut;
            newInstanceFormation.DateFin = selectedDateFin;
            newInstanceFormation.Formation = selectedFormation;
            newInstanceFormation.IdFormation = selectedFormation.IdFormation;
            instanceFormationDataService.CreateElement(newInstanceFormation);
            
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());
            //MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Send(new ReturnToInstanceFormationsViewMessage());            
        }

        private bool CanSaveNewInstanceFormation(object obj)
        {
            
            if(selectedFormation != null)                
                return true;            
            return false;
        }

        private void CancelNewInstanceFormation(object obj)
        {
            newInstanceFormation = new InstanceFormation();
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());            
        }

        private bool CanCancelNewInstanceFormation(object obj)
        {
            return true;
        }


        #endregion
    }
}
