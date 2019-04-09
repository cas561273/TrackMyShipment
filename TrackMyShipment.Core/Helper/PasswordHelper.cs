using System;
using System.Security.Cryptography;
using System.Text;

namespace TrackMyShipment.Core.Helper
{
    public static class PasswordHelper
    {
        public static string CalculateHashedPassword(string email, string pass)
        {
            HashAlgorithm hash = new SHA256Managed();

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pass + email);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);
            string hashValue = Convert.ToBase64String(hashBytes);

            pass = hashValue;

            return pass;
        }
    }
}