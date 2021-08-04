using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransit.Contracts.TransferObjects
{
    public class AccountViewModel
    {
        public Guid UserID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}