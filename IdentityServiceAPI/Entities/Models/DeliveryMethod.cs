using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class DeliveryMethod
    {
        [Column("DeliveryMethodID")]
        [Key]
        public Guid DeliveryMethodID { get; set; }

        [Required(ErrorMessage = "Delivery method name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the delivery method name is 60 characters")]
        public string DeliveryMethodName { get; set; }
    }
}
