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
    public class FormationViewModel : ViewModelBase
    {
        #region Déclaration des champs

        private FormationDataService formationDataService;
        private ObservableCollection<Formation> formations;
        private Formation selectedFormation;

        #endregion

        #region Les proppriétés

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

        #endregion

        #region Les commandes

        public ICommand EditFormationCommand { get; set; }
        public ICommand CreateFormationCommand { get; set; }

        #endregion

        #region Le constructeur

        public FormationViewModel()
        {
            formationDataService = new FormationDataService();

            LoadFormations();

            LoadCommands();

            MyMessenger<UpdateFormationsListMessage>.Instance.Register(OnUpdateFormationsListMessageReceived);
        }        

        #endregion

        #region Les méthodes

        private void LoadFormations()
        {
            Formations = formationDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            EditFormationCommand = new RelayCommand(EditFormation, CanEditFormation);
            CreateFormationCommand = new RelayCommand(AddFormation, CanAddFormation);
        }

        private void EditFormation(object obj)
        {
            EditFormationMessage message = new EditFormationMessage { SelectedFormation = selectedFormation };

            MyMessenger<ShowFormationDetailMessage>.Instance.Send(new ShowFormationDetailMessage());

            MyMessenger<EditFormationMessage>.Instance.Send(message);
        }

        private bool CanEditFormation(object obj)
        {
            if (selectedFormation != null)
                return true;
            return false;
        }

        private void AddFormation(object obj)
        {
            MyMessenger<CreateNewFormationMessage>.Instance.Send(new CreateNewFormationMessage());
        }

        private bool CanAddFormation(object obj)
        {
            return true;
        }

        private void OnUpdateFormationsListMessageReceived(UpdateFormationsListMessage obj)
        {
            LoadFormations();
        }

        #endregion
    }
}
