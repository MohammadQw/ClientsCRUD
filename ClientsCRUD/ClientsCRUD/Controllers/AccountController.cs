using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClientsCRUD.Models;
using ClientsCRUD.DTOs;
namespace ClientsCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        /// <summary>
        /// For Register User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                await _userManager.AddToRoleAsync(user, model.Role);

                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login User 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid login details.");
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { message = "User logged in successfully" });
            }

            return Unauthorized("Invalid login attempt.");
        }
    }
}
