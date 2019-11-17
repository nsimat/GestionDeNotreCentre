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
    public class FormateurViewModel : ViewModelBase
    {
        #region Déclaration des champs

        private FormateurDataService formateurDataService;
        private PersonneDataService personneDataService;
        private ObservableCollection<Formateur> formateurs;
        private Formateur selectedFormateur;

        #endregion

        #region Les propriétés

        public ObservableCollection<Formateur> Formateurs
        {
            get => formateurs;
            set
            {
                formateurs = value;
                RaisePropertyChanged(nameof(Formateurs));
            }
        }

        public Formateur SelectedFormateur
        {
            get => selectedFormateur;
            set
            {
                selectedFormateur = value;
                RaisePropertyChanged(nameof(SelectedFormateur));
            }
        }

        #endregion

        #region Les commandes

        public ICommand EditFormateurCommand { get; set; }
        public ICommand CreateFormateurCommand { get; set; }

        #endregion        

        #region Le constructeur

        public FormateurViewModel()
        {
            formateurDataService = new FormateurDataService();


            LoadFormateurs();

            LoadCommands();

            //Messenger.Default.Register<UpdateModulesListMessage>(this, OnUpdateModulesListMessageReceived);
            MyMessenger<UpdateFormateursListMessage>.Instance.Register(OnUpdateFormateursListMessageReceived);

        }

        #endregion

        #region Les méthodes

        private void OnUpdateFormateursListMessageReceived(UpdateFormateursListMessage obj)
        {
            LoadFormateurs();
        }

        private void LoadFormateurs()
        {
            Formateurs = formateurDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            EditFormateurCommand = new RelayCommand(EditFormateur, CanEditFormateur);
            CreateFormateurCommand = new RelayCommand(AddFormateur, CanAddFormateur);
        }

        private void EditFormateur(object obj)
        {
            //Créer et envoyer une demande d'édition
            EditFormateurMessage message = new EditFormateurMessage { SelectedFormateur = this.SelectedFormateur };
            MyMessenger<EditFormateurMessage>.Instance.Send(message);

            //Envoyer une demande d'édition au MainViewModel
            MyMessenger<ShowFormateurDetailMessage>.Instance.Send(new ShowFormateurDetailMessage());            
        }

        private bool CanEditFormateur(object obj)
        {
            if (selectedFormateur != null)
                return true;
            return false;
        }

        private void AddFormateur(object obj)
        {
            //Envoyer une demande d'affichage de la vue de création d'un nouveau formateur
            MyMessenger<CreateNewFormateurMessage>.Instance.Send(new CreateNewFormateurMessage());
        }

        private bool CanAddFormateur(object obj)
        {
            return true;
        }

        #endregion
    }
}
