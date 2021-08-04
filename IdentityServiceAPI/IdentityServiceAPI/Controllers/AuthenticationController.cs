using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
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
        private readonly IRequestClient<AccountRequest> _requestClient;

        public AuthenticationController(ILoggerManager loggerManager, IMapper mapper , UserManager<User> userManager, IAuthenticationManager authManager, IRequestClient<AccountRequest> requestClient)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _requestClient = requestClient;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userForRegistration">User info for registration</param>
        /// <returns>Request status</returns>
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

        /// <summary>
        /// Log in
        /// </summary>
        /// <remarks>
        /// Sample login (Admin):
        ///
        ///     POST authentication/login
        ///     {
        ///       "userName": "Andrew2255",
        ///       "password": "Qwerty123456789"
        ///     }
        ///
        /// Sample login (Customer):
        ///
        ///     POST authentication/login
        ///     {
        ///       "userName": "Simple_Gy",
        ///       "password": "Qwerty123456789"
        ///     }
        /// </remarks>
        /// <param name="user">User info for authentication</param>
        /// <returns>Request status</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _loggerManager.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");

                return Unauthorized();
            }

            var tempUser = await _userManager.FindByNameAsync(user.UserName);
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(tempUser);
            var model = new AccountViewModel
            {
                UserID = Guid.Parse(tempUser.Id),
                UserName = tempUser.UserName,
                PhoneNumber = tempUser.PhoneNumber,
                Email = tempUser.Email,
                Role = roles.Count > 1 ? String.Join(" - ", roles.ToArray()) : roles[0]
            };
            var response = await _requestClient.GetResponse<AccountResponse>(new AccountRequest { AccountModel = model });

            return Ok(new
            {
                Token = await _authManager.CreateToken(),
                RefreshToken = _authManager.GenerateRefreshToken()
            });
        }
    }
}
