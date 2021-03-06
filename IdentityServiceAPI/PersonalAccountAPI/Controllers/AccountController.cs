using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalAccountAPI.Controllers
{
    [Route("/account")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Authorize]

    public class AccountController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private IConfiguration _configuration;
        private readonly IRequestClient<AccountRequest> _requestClient;

        public AccountController(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration, IRequestClient<AccountRequest> requestClient)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
            _requestClient = requestClient;
        }

        /// <summary>
        /// Get the list of Accounts
        /// </summary>
        /// <param name="parameters">Input parameters such as page size and page number</param>
        /// <returns>The list of Accounts</returns>
        [HttpGet("all", Name = "GetAccounts")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountParameters parameters)
        {
            var accounts = await _repository.AccountRepository.GetAccountsAsync(parameters, trackChanges: false);

            return Ok(new
            {
                Accounts = accounts,
                TotalCount = accounts.MetaData.TotalCount
            });
        }

        /// <summary>
        /// Get Account record by Id
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Account record</returns>
        [HttpGet("{id}", Name = "AccountById")]
        [Authorize(Roles = "Admin")]
        public async Task<Account> GetAccount(Guid id)
        {
            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);
            if (account == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return null;
            }
            else
            {
                return account;
            }
        }

        /// <summary>
        /// Get Customer Account record by Id
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Account record</returns>
        [HttpGet]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<Account> GetPersonalAccount()
        {
            Guid id = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);
            if (account == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return null;
            }
            else
            {
                return account;
            }
        }

        /// <summary>
        /// Delete Account record
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>String message about execution status</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);

            if (account != null)
            {
                var model = new AccountViewModel
                {
                    UserID = account.UserID,
                    UserName = account.UserName,
                    PhoneNumber = account.PhoneNumber,
                    Email = account.Email,
                    Role = account.Role,
                    Operation = "DELETE"
                };
                var response = await _requestClient.GetResponse<AccountResponse>(new AccountRequest { AccountModel = model });

                _repository.AccountRepository.DeleteAccount(account);

                await _repository.SaveAsync();

                return Ok();
            }
            else
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in database");
                return NotFound($"Account with id {id} doesn't exist in the database");
            }
        }

        /// <summary>
        /// Update Account record
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="account">Account data</param>
        /// <returns>String message about execution status</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] Account account)
        {
            var user = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);
            if (user != null)
            {
                AccountViewModel model = new AccountViewModel
                {
                    UserID = account.UserID,
                    UserName = account.UserName,
                    PhoneNumber = account.PhoneNumber,
                    Email = account.Email,
                    Role = account.Role,
                    Operation = "UPDATE"
                };

                var response = await _requestClient.GetResponse<AccountResponse>(new AccountRequest { AccountModel = model });

                _repository.AccountRepository.UpdateAccount(account);

                await _repository.SaveAsync();

                return Ok();
            }
            else
            {
                _logger.LogInfo($"Account with id {id} doesn't exist in the database");
                return NotFound($"Account with id {id} doesn't exist in the database");
            }
        }

        /// <summary>
        /// Create new Account record
        /// </summary>
        /// <param name="account">Account data for creation</param>
        /// <returns>String message about execution status</returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (await _repository.AccountRepository.GetAccountAsync(account.UserID, trackChanges: false) == null)
            {
                _repository.AccountRepository.CreateAccount(account);

                await _repository.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest($"Account with id {account.UserID} is already exist");
            }
        }
    }
}
