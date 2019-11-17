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
    public class InstanceFormationViewModel : ViewModelBase
    {
        #region Déclaration des champs

        private readonly InstanceFormationDataService instanceFormationDataService;
        private PersonneDataService personneDataService;
        private ObservableCollection<InstanceFormation> instanceFormations;
        private InstanceFormation selectedInstanceFormation;

        #endregion

        #region Les propriétés

        public ObservableCollection<InstanceFormation> InstanceFormations
        {
            get => instanceFormations;
            set
            {
                instanceFormations = value;
                RaisePropertyChanged(nameof(InstanceFormations));
            }
        }

        public InstanceFormation SelectedInstanceFormation
        {
            get => selectedInstanceFormation;
            set
            {
                selectedInstanceFormation = value;
                RaisePropertyChanged(nameof(SelectedInstanceFormation));
            }
        }

        #endregion

        #region Les commandes

        public ICommand EditInstanceFormationCommand { get; set; }
        public ICommand CreateInstanceFormationCommand { get; set; }

        #endregion

        #region Le constructeur

        public InstanceFormationViewModel()
        {
            instanceFormationDataService = new InstanceFormationDataService();

            LoadInstanceFormations();

            LoadCommands();

            MyMessenger<UpdateInstanceFormationsListMessage>.Instance.Register(OnUpdateInstanceFormationsListMessageReceived);
        }

        #endregion

        #region Les méthodes

        private void LoadInstanceFormations()
        {
            InstanceFormations = instanceFormationDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            EditInstanceFormationCommand = new RelayCommand(EditInstanceFormation, CanEditInstanceFormation);
            CreateInstanceFormationCommand = new RelayCommand(CreateInstanceFormation, CanCreateInstanceFormation);
    }

        private void EditInstanceFormation(object obj)
        {
            //Créer et envoyer une demande d'édition

            EditInstanceFormationMessage message = new EditInstanceFormationMessage { SelectedInstanceFormation = this.SelectedInstanceFormation };
            MyMessenger<EditInstanceFormationMessage>.Instance.Send(message);

            //Envoyer une demande d'édition au MainViewModel
            MyMessenger<ShowInstanceFormationDetailMessage>.Instance.Send(new ShowInstanceFormationDetailMessage());
        }

        private bool CanEditInstanceFormation(object obj)
        {
            return true;
        }

        private void CreateInstanceFormation(object obj)
        {
            //Envoyer une demande d'affichage de la vue de création d'une nouvelle instance de formation
            MyMessenger<CreateNewInstanceFormationMessage>.Instance.Send(new CreateNewInstanceFormationMessage());
        }

        private bool CanCreateInstanceFormation(object obj)
        {
            return true;
        }

        private void OnUpdateInstanceFormationsListMessageReceived(UpdateInstanceFormationsListMessage obj)
        {
            LoadInstanceFormations();
        }

        #endregion


    }
}
