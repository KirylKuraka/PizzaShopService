using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
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
        public async Task<IEnumerable<Account>> GetAccounts([FromQuery] AccountParameters parameters)
        {
            var accounts = await _repository.AccountRepository.GetAccountsAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(accounts.MetaData));

            return accounts;
        }

        [HttpGet("{id}", Name = "AccountById")]
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

        [HttpDelete("{id}")]
        public async Task<string> DeleteAccount(Guid id)
        {
            var account = await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false);

            if (account != null)
            {
                _repository.AccountRepository.DeleteAccount(account);

                await _repository.SaveAsync();

                return "Account was deleted";
            }
            else
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in database");
                return $"Account with id {id} doesn't exist in the database";
            }
        }

        [HttpPut("{id}")]
        public async Task<string> UpdateAccount(Guid id, [FromBody] Account account)
        {
            if (await _repository.AccountRepository.GetAccountAsync(id, trackChanges: false) != null)
            {
                _repository.AccountRepository.UpdateAccount(account);

                await _repository.SaveAsync();

                return "Account was updated";
            }
            else
            {
                _logger.LogInfo($"Account with id {id} doesn't exist in the database");
                return $"Account with id {id} doesn't exist in the database";
            }
        }

        [HttpPost]
        public async Task<string> CreateAccount([FromBody] Account account)
        {
            if (await _repository.AccountRepository.GetAccountAsync(account.UserID, trackChanges: false) == null)
            {
                _repository.AccountRepository.CreateAccount(account);

                await _repository.SaveAsync();
                return "Account was created";
            }
            else
            {
                return $"Account with id {account.UserID} is already exist";
            }
        }
    }
}
