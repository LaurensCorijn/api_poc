using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using api_poc.DTO;
using api_poc.Models;
using api_poc.Models.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api_poc.Controllers
{ 
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUserService userService, IConfiguration config)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._userService = userService;
            this._config = config;
        }

        ///<summary>
        /// Login
        /// </summary>
        /// <param name="model">The login details</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token);
                }
            }
            return BadRequest();
        }

        private String GetToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        ///<summary>
        /// Register
        /// </summary>
        /// <param name="model">The user details</param>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            IdentityUser _user = new IdentityUser { UserName = model.Email, Email = model.Email };
            User user = new User(model.userName, model.Email);
            var result = await _userManager.CreateAsync(_user, model.Password);

            if (result.Succeeded)
            {
                _userService.Add(user);
                _userService.SaveChanges();
                string token = GetToken(_user);
                return Created("", token);
            }
            return BadRequest();
        }

        ///<summary>
        /// Checks if an email is available as username
        /// </summary>
        /// <returns>True if the email is not registered yet</returns>
        /// <param name="email">Email</param>
        [AllowAnonymous]
        [HttpGet("Checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }
    }
}
