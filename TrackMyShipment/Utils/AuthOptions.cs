using Microsoft.IdentityModel.Tokens;

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
        public static string secretKey = "craftkey91Keys13";
        public static SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        public static SigningCredentials signinCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
