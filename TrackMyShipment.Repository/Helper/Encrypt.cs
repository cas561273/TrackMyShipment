using System;
using System.Security.Cryptography;
using System.Text;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Core.Utils
{
    public static class Encrypt
    {
        public static string Sha256(string email,string pass)
        {
            HashAlgorithm hash = new SHA256Managed();

            // compute hash of the password prefixing password with the salt
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pass + email);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);
            string hashValue = Convert.ToBase64String(hashBytes);
            pass = hashValue;

            return pass;
        }
    }
}
