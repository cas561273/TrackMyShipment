using System;
using System.Security.Cryptography;
using System.Text;

namespace TrackMyShipment.Repository.Helper
{
    public static class Encrypt
    {
        public static string Sha256(string email, string pass)
        {
            HashAlgorithm hash = new SHA256Managed();

            // compute hash of the password prefixing password with the salt
            var plainTextBytes = Encoding.UTF8.GetBytes(pass + email);
            var hashBytes = hash.ComputeHash(plainTextBytes);
            var hashValue = Convert.ToBase64String(hashBytes);
            pass = hashValue;

            return pass;
        }
    }
}