using GestionDeCentreDAL.Models;
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

        private InstanceFormation newInstanceFormation; //L'instance de formation que l'on veut créer  
        private ObservableCollection<Formation> formations;//
        private Formation selectedFormation;
        private ObservableCollection<string> formationStatuts;
        private Personne responsable;

        #endregion

        #region Les commandes

        public ICommand SaveNewInstanceFormationCommand { get; set; }
        public ICommand CancelNewInstanceFormationCommand { get; set; }

        #endregion

        #region Les propriétés

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

            LoadCommands();
        }

        #endregion

        #region Les méthodes

        private void LoadCommands()
        {
            SaveNewInstanceFormationCommand = new RelayCommand(SaveNewInstanceFormation, CanSaveNewInstanceFormation);
            CancelNewInstanceFormationCommand = new RelayCommand(CancelNewInstanceFormation, CanCancelNewInstanceFormation);
        }

        private void SaveNewInstanceFormation(object obj)
        {
            instanceFormationDataService.CreateElement(newInstanceFormation);
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());
            //MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Send(new ReturnToInstanceFormationsViewMessage());
        }

        private bool CanSaveNewInstanceFormation(object obj)
        {
            return true;
        }

        private void CancelNewInstanceFormation(object obj)
        {
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Send(new DisplayInstanceFormationViewMessage());
            //MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Send(new ReturnToInstanceFormationsViewMessage());
        }

        private bool CanCancelNewInstanceFormation(object obj)
        {
            return true;
        }


        #endregion
    }
}
