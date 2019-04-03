using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Utils
{
    public class Token
    {
        public string GetToken(User user)
        {
            SymmetricSecurityKey secretKey = AuthOptions.IssuerSigningKey;
            SigningCredentials signinCredentials = AuthOptions.signinCredentials;

            List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Role),
            };

            JwtSecurityToken tokeOptions = new JwtSecurityToken(
                issuer: AuthOptions.ValidIssuer,
                audience: AuthOptions.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(50000),
                signingCredentials: signinCredentials
             );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
