using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DALGestionDeCentre.Services
{
    public static class HashingMotDePasse
    {
        public static string EncodageMD5(string motDePasse)
        {
            string motDePasseSel = "PersonneInscrit" + motDePasse + "ASP.NET MVC 5";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}
