using Contracts;
using Entities.Models;
using MassTransit;
using MassTransit.Contracts.TransferObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServiceAPI.MassTransit
{
    public class IdentityConsumer : IConsumer<AccountRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _loggerManager;

        public IdentityConsumer(ILoggerManager loggerManager, UserManager<User> userManager)
        {
            _loggerManager = loggerManager;
            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<AccountRequest> context)
        {
            var message = context.Message.AccountModel;

            bool responseResult = false;
            if (message != null)
            {
                User user = await _userManager.FindByIdAsync(message.UserID.ToString());
                if (user != null)
                {
                    switch (message.Operation)
                    {
                        case "DELETE":
                            await _userManager.DeleteAsync(user);
                            break;
                        case "UPDATE":
                            user.Email = message.Email;
                            user.UserName = message.UserName;
                            user.PhoneNumber = message.PhoneNumber;

                            await _userManager.UpdateAsync(user);

                            var messageRoles = message.Role.Trim().Split(" - ");
                            var roles = await _userManager.GetRolesAsync(user);

                            await _userManager.RemoveFromRolesAsync(user, roles.Except(messageRoles));
                            await _userManager.AddToRolesAsync(user, messageRoles.Except(roles));

                            break;
                        default:
                            break;
                    }
                }

                responseResult = true;
            }

            await context.RespondAsync<AccountResponse>(new AccountResponse { ResponseResult = responseResult });
        }
    }
}
