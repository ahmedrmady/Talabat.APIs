using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
  

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _athService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAuthService athService
            
            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._athService = athService;
        }


        [HttpPost("login")] // POST : /api/Account
        public async Task<ActionResult<UserDto>> Login (LoginDto model)
        {

            var user =await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(
                new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _athService.CreateTokenAsync(user,_userManager)

                }); ;

        }


        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register (RegisterDto model)
        {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.phoneNumber,
                UserName = model.Email.Split('@')[0]
            };

            var result =  await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(
                new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _athService.CreateTokenAsync(user,_userManager)
                }
                ) ;
        }




    }
}
