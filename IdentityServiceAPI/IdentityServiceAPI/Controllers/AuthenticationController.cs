using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServiceAPI.Controllers
{
    [Route("/authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : Controller
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(ILoggerManager loggerManager, IMapper mapper , UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return Ok(user);
            //return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _loggerManager.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");

                return Unauthorized();
            }

            return Ok(new
            {
                Token = await _authManager.CreateToken()
            });
        }
    }
}
