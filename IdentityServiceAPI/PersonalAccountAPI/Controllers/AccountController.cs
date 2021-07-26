using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAccountAPI.Controllers
{
    [Route("/account")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class AccountController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public AccountController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAccounts")]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountParameters parameters)
        {
            var accounts = await _repository.AccountRepository.GetAccountsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(accounts.MetaData));

            return Ok(accounts);
        }

        [HttpGet("{id}", Name = "AccountById")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);
            if (account == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);

            if (account != null)
            {
                _repository.AccountRepository.DeleteAccount(account);

                await _repository.SaveAsync();

                return NoContent();
            }
            else
            {
                return NotFound($"Account with id {id} doesn't exist in the database");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            if (await _repository.AccountRepository.GetAccountAsync(account.UserID, trackChanges: false) != null)
            {
                _repository.AccountRepository.UpdateAccount(account);

                await _repository.SaveAsync();

                return Ok("Account was updated");
            }
            else
            {
                return NotFound($"Account with id {account.UserID} doesn't exist in the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (await _repository.AccountRepository.GetAccountAsync(account.UserID, trackChanges: false) == null)
            {
                _repository.AccountRepository.CreateAccount(account);

                await _repository.SaveAsync();
                return Ok("Account was created");
            }
            else
            {
                return NotFound($"Account with id {account.UserID} is already exist");
            }
        }
    }
}
