using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //add the privet clamis
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.UserData,user.UserName)
            };

            //add roles
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            //add symmetricKey
            var symetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));

            var sigInCreditail = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256Signature);

            // create jwtObject 
            var jwtObject = new JwtSecurityToken(
                
                audience: _configuration["JWT:VailedAudince"],

                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:Expireation"])),
                
                claims: authClaims,
                signingCredentials: sigInCreditail


                );
            return new JwtSecurityTokenHandler().WriteToken(jwtObject);

        }

    }
}
