using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransit.Contracts.TransferObjects
{
    public class UserRequest
    {
        public Guid UserID { get; set; }
    }
}
