using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrackMyShipment.Utils
{
    public class AuthOptions
    {
        public static bool ValidateIssuer = false;
        public static bool ValidateAudience = false;
        public static bool ValidateLifetime = true;
        public static bool ValidateIssuerSigningKey = true;
        public static string ValidIssuer = "https://localhost:44395";
        public static string ValidAudience = "https://localhost:44395";
        public static string SecretKey = "craftkey91Keys13";

        public static SymmetricSecurityKey IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public static SigningCredentials SigninCredentials =
            new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature);
    }
}