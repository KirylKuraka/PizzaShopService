using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransit.Contracts.TransferObjects
{
    public class AccountRequest
    {
        public AccountViewModel AccountModel { get; set; }
    }
}