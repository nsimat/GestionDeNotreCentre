using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly LoginDataService loginService;
        private readonly LoginDialogService dialogService;
        private string _adresseMail;
        
        public ICommand LoginCommand { get; set; }

        public string AdresseMail
        {
            get
            {
                return _adresseMail;
            }

            set
            {
                _adresseMail = value;
                RaisePropertyChanged(nameof(AdresseMail));
            }
        }
        
        public bool Authentified { get; set; }
        public Personne Employe { get; set; }

        public LoginViewModel()
        {
            loginService = new LoginDataService();
            dialogService = new LoginDialogService();

            LoadLoginCommand();
        }

        private void LoadLoginCommand()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private void Login(object obj)
        {
            var passwordBox = obj as PasswordBox;
            var password = passwordBox.Password;
            passwordBox.Clear();

            MessageBox.Show("Welcome " + AdresseMail);

            Employe = loginService.Authentifier(AdresseMail, password);

            if (Employe != null)
            {
                Authentified = true;
                PersonneAuthenticatedMessage result = new PersonneAuthenticatedMessage()
                {
                    Employe = Employe,
                    Authenticated = Authentified
                };
                //var window = Application.Current.MainWindow;
                Messenger.Default.Send<PersonneAuthenticatedMessage>(result);
                //dialogService.ShowDialog();
                //MainWindow dashboard = new MainWindow();
                //dashboard.Show();
                

            }
            else
            {
                MessageBox.Show("L'adresse ou le mot de passe est incorrect.");
            }
        }

        private bool CanLogin(object obj)
        {
            return true;//!string.IsNullOrEmpty(AdresseMail) && !string.IsNullOrEmpty((obj as PasswordBox).Password )
        }

        //public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        //{
        //    bool validityFlag = new Validator().IsValidEmail(value.ToString());
        //    return new ValidationResult(validityFlag, validityFlag ? null : "SVP, entrez un email valide.");
        //}
    }
}
