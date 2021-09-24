using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class PaymentMethod
    {
        [Column("PaymentMethodID")]
        [Key]
        public Guid PaymentMethodID { get; set; }

        [Required(ErrorMessage = "Payment method name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the payment method name is 60 characters")]
        public string PaymentMethodName { get; set; }
    }
}
