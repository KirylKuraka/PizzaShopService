using AutoMapper;
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
    public class UserConsumer: IConsumer<UserRequest>
    {
        private readonly ILoggerManager _loggerManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;

        public UserConsumer(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        public async Task Consume(ConsumeContext<UserRequest> context)
        {
            var id = context.Message.UserID.ToString();
            var user = await _userManager.FindByIdAsync(id);

            UserViewModel model = new UserViewModel {
                UserID = Guid.Parse(user.Id),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            await context.RespondAsync<UserResponse>(new UserResponse { User = model });
        }
    }
}
