using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using MassTransit.Contracts.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServiceAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDTO, User>();
            CreateMap<UserViewModel, User>();
        }
    }
}
