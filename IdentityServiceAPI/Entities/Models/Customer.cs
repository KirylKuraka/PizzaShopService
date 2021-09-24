using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Customer
    {
        [Column("CustomerID")]
        [Key]
        public Guid CustomerID { get; set; }

        [Required(ErrorMessage = "Customer name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the customer name is 60 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone number is a required field")]
        [MaxLength(18, ErrorMessage = "Maximum length for the phone number is 18 characters")]
        public string PhoneNumber { get; set; }
    }
}
