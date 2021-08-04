using Contracts;
using Entities.Models;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAccountAPI.MassTransit
{
    public class AccountConsumer : IConsumer<AccountRequest>
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryManager _repository;

        public AccountConsumer(ILoggerManager loggerManager,IRepositoryManager repository)
        {
            _loggerManager = loggerManager;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<AccountRequest> context)
        {
            var account = context.Message.AccountModel;

            bool responseResult = false;
            if (account != null)
            {
                var temp = new Account
                {
                    UserID = account.UserID,
                    UserName = account.UserName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    Role = account.Role
                };

                if (await _repository.AccountRepository.GetAccountAsync(account.UserID, trackChanges: false) == null)
                {
                    _repository.AccountRepository.CreateAccount(temp);
                }
                /*else
                {
                    _repository.AccountRepository.UpdateAccount(temp);
                }*/

                await _repository.SaveAsync();
                responseResult = true;
            }

            await context.RespondAsync<AccountResponse>(new AccountResponse { ResponseResult = responseResult });
        }
    }
}