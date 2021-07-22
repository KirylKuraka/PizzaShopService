using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForRegistrationDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Email { get; set;  }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }

    }
}
