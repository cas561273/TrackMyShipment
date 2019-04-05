using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Utils
{
    public class Token
    {
        public string GetToken(User user)
        {
            var signinCredentials = AuthOptions.SigninCredentials;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var tokeOptions = new JwtSecurityToken(
                AuthOptions.ValidIssuer,
                AuthOptions.ValidAudience,
                claims,
                expires: DateTime.Now.AddMinutes(50000),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}