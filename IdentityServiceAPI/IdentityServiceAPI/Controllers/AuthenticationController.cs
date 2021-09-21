using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager, IRequestClient<AccountRequest> requestClient,
                                        IConfiguration configuration)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _requestClient = requestClient;
            _configuration = configuration;
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
            var model = new AccountViewModel
            {
                UserID = Guid.Parse(tempUser.Id),
                UserName = tempUser.UserName,
                PhoneNumber = tempUser.PhoneNumber,
                Email = tempUser.Email
            };
            var response = await _requestClient.GetResponse<AccountResponse>(new AccountRequest { AccountModel = model });

            var token = await _authManager.CreateToken();
            var refreshToken = _authManager.GenerateRefreshToken(32);

            string providerName, refreshTokenName, refreshTokenExpiryTimeName, expiryTime;
            GetRefreshSettings(out providerName, out refreshTokenName, out refreshTokenExpiryTimeName, out expiryTime);

            await _userManager.RemoveAuthenticationTokenAsync(tempUser, providerName, refreshTokenName);
            await _userManager.GenerateUserTokenAsync(tempUser, providerName, refreshTokenName);
            await _userManager.SetAuthenticationTokenAsync(tempUser, providerName, refreshTokenName, refreshToken);

            await _userManager.RemoveAuthenticationTokenAsync(tempUser, providerName, refreshTokenExpiryTimeName);
            await _userManager.GenerateUserTokenAsync(tempUser, providerName, refreshTokenExpiryTimeName);
            await _userManager.SetAuthenticationTokenAsync(tempUser, providerName, refreshTokenExpiryTimeName, DateTime.Now.AddMinutes(Double.Parse(expiryTime)).ToString());

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Get refresh settings form configuration file
        /// </summary>
        /// <param name="providerName">Provider name</param>
        /// <param name="refreshTokenName">Refresh token field name</param>
        /// <param name="refreshTokenExpiryTimeName">Refresh token expiry time field name</param>
        /// <param name="expiryTime">Refresh token expiry time</param>
        private void GetRefreshSettings(out string providerName, out string refreshTokenName, out string refreshTokenExpiryTimeName, out string expiryTime)
        {
            var refreshSettings = _configuration.GetSection("RefreshTokenSettings");
            providerName = refreshSettings.GetSection("ServiceName").Value;
            refreshTokenName = refreshSettings.GetSection("RefreshTokenName").Value;
            refreshTokenExpiryTimeName = refreshSettings.GetSection("ExpiryTimeName").Value;
            expiryTime = _configuration.GetSection("JwtSettings").GetSection("expires").Value;
        }

        /// <summary>
        /// Create new token and refresh token
        /// </summary>
        /// <param name="tokenModel">Token model that includes old token and old refresh token</param>
        /// <returns>token and refresh token</returns>
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return NoContent();
            }

            string accessToken = tokenModel.Token;
            string refreshToken = tokenModel.RefreshToken;

            var principal = _authManager.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                string providerName, refreshTokenName, refreshTokenExpiryTimeName, time;
                GetRefreshSettings(out providerName, out refreshTokenName, out refreshTokenExpiryTimeName, out time);

                var mainRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, providerName, refreshTokenName);
                var expiryTime = DateTime.Parse(await _userManager.GetAuthenticationTokenAsync(user, providerName, refreshTokenExpiryTimeName));

                if (!refreshToken.Equals(mainRefreshToken) || expiryTime <= DateTime.Now)
                {
                    return NoContent();
                }
                else
                {
                    _authManager.SetUser(user);
                    var newToken = await _authManager.CreateToken();
                    var newRefreshToken = _authManager.GenerateRefreshToken(32);

                    _configuration.GetSection("JwtSettings");

                    await _userManager.SetAuthenticationTokenAsync(user, providerName, refreshTokenName, newRefreshToken);
                    await _userManager.SetAuthenticationTokenAsync(user, providerName, refreshTokenExpiryTimeName, DateTime.Now.AddMinutes(Double.Parse(time)).ToString());

                    return new ObjectResult(new
                    {
                        Token = newToken,
                        RefreshToken = newRefreshToken
                    });
                }
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Delete refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return BadRequest();

            string providerName, refreshTokenName, refreshTokenExpiryTimeName, time;
            GetRefreshSettings(out providerName, out refreshTokenName, out refreshTokenExpiryTimeName, out time);

            await _userManager.RemoveAuthenticationTokenAsync(user, providerName, refreshTokenName);
            await _userManager.RemoveAuthenticationTokenAsync(user, providerName, refreshTokenExpiryTimeName);

            return NoContent();
        }

        /// <summary>
        /// Get user roles by userID
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>All roles that was assigned to user</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

                return Ok(new
                {
                    Role = roles.Count > 1 ? String.Join(" - ", roles.ToArray()) : roles[0]
                });
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Сhecks for the existence of the user name
        /// </summary>
        /// <param name="id">User id for excluding a specific user</param>
        /// <param name="userName">Username to check</param>
        /// <returns>true - exits, false - doesn't exist</returns>
        [HttpGet("checkUsername")]
        [Authorize]
        public async Task<bool> CheckUsername(Guid id, string userName)
        {
            bool isExists = false;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    isExists = !user.Id.Equals(id.ToString()) ? true : false;
                }
            }

            return isExists;
        }
    }
}
