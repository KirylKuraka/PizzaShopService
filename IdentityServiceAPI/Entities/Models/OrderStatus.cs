using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class OrderStatus
    {
        [Column("OrderStatusID")]
        [Key]
        public Guid OrderStatusID { get; set; }

        [Required(ErrorMessage = "Order status name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the order status name is 60 characters")]
        public string OrderStatusName { get; set; }
    }
}
