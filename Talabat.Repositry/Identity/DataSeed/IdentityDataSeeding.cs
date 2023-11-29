using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositry.Identity.DataSeed
{
    public static class IdentityDataSeeding
    {

        public static async Task SeedIdentityDataAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Rmady",
                    Email = "ahmedrmady0@gmail.com",
                    UserName = "Ahmed.Rmady",
                    PhoneNumber = "0111223567"

                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }

        }
    }
}
