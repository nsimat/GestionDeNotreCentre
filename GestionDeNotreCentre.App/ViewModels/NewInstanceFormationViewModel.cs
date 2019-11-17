using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
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

        private InstanceFormation newInstanceFormation; //L'instance de formation que l'on veut créer         

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

        #endregion

        #region Le constructeur

        public NewInstanceFormationViewModel()
        {
            newInstanceFormation = new InstanceFormation();
            instanceFormationDataService = new InstanceFormationDataService();

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
            MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Send(new ReturnToInstanceFormationsViewMessage());
        }

        private bool CanSaveNewInstanceFormation(object obj)
        {
            return true;
        }

        private void CancelNewInstanceFormation(object obj)
        {
            MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Send(new ReturnToInstanceFormationsViewMessage());
        }

        private bool CanCancelNewInstanceFormation(object obj)
        {
            return true;
        }


        #endregion
    }
}
