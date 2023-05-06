using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Constants;
using WebShop.Data.Entites.Identity;
using WebShop.Helpers;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;

        public AccountController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model)
        {
            var imageName = string.Empty;
            if(!string.IsNullOrEmpty(model.ImageBase64))
            {
                imageName = ImageWorker.SaveImage(model.ImageBase64);
            }
            UserEntity user = new UserEntity()
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                Image = imageName,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.User);
                return Ok();
            }
            
            return BadRequest();
        }
    }
}
