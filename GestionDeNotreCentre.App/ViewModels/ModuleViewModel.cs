using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Extensions;
using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class ModuleViewModel : ViewModelBase
    {
        #region Déclaration des champs

        private ModuleDataService moduleDataService;
        private ModuleDialogservice moduleDialogservice;
        private ObservableCollection<Module> modules;
        private Module selectedModule;

        #endregion

        #region Les propriétés

        public ObservableCollection<Module> Modules 
        { 
            get => modules;
            set
            { 
                modules = value;
                RaisePropertyChanged(nameof(Modules));
            }
        }

        public Module SelectedModule 
        { 
            get => selectedModule;
            set 
            { 
                selectedModule = value;
                RaisePropertyChanged(nameof(SelectedModule));
            }
        }

        #endregion

        #region Les commandes

        public ICommand EditModuleCommand { get; set; }
        public ICommand CreateModuleCommand { get; set; }

        #endregion        

        #region Le constructeur

        public ModuleViewModel()
        {
            moduleDataService = new ModuleDataService();
            moduleDialogservice = new ModuleDialogservice();

            LoadModules();

            LoadCommands();
            
            MyMessenger<UpdateModulesListMessage>.Instance.Register(OnUpdateModulesListMessageReceived);            
        }

        #endregion

        #region Les méthodes

        private void OnUpdateModulesListMessageReceived(UpdateModulesListMessage obj)
        {
            LoadModules();            
        }

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();            
        }

        private void LoadCommands()
        {
            EditModuleCommand = new RelayCommand(EditModule, CanEditModule);
            CreateModuleCommand = new RelayCommand(AddModule, CanAddModule);
        }

        private void AddModule(object obj)
        {
            MyMessenger<CreateNewModuleMessage>.Instance.Send(new CreateNewModuleMessage());
        }

        private bool CanAddModule(object obj)
        {
            return true;
        }

        private bool CanEditModule(object obj)
        {
            if (selectedModule != null)
                return true;
            return false;
        }

        private void EditModule(object obj)
        {
            EditModuleMessage message = new EditModuleMessage { SelectedModule = selectedModule };            

            MyMessenger<ShowModuleDetailViewMessage>.Instance.Send(new ShowModuleDetailViewMessage());
            MyMessenger<EditModuleMessage>.Instance.Send(message);
        }

        #endregion
    }
}
