using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Utility
{
    public class Validator
    {
        public Validator()
        {

        }
        public string IsValid(string email, string password)
        {
            if (this.IsLoginDataEmpty(email, password))
                return "SVP, saisissez vos données de connexion.";

            if (!this.IsValidEmail(email))
                return "SVP, saisissez une adresse electronique valide.";

            if (!this.IsValidPassword(password))
                return "SVP, saisissez un mot de passe valide.";

            return string.Empty;
        }
        public bool IsValidUserLogin(string email)
        {
            return !(string.IsNullOrWhiteSpace(email) || IsValidEmail(email) || email.Length < 4 || email.Length > 25);
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool IsLoginDataEmpty(string email, string password)
        {
            return string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password);
        }

        public bool IsValidPassword(string password)
        {
            return !(string.IsNullOrEmpty(password) || password.Length < 5 || password.Length > 25);
        }
    }
}
