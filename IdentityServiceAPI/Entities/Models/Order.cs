using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrderID")]
        [Key]
        public Guid OrderID { get; set; }

        [ForeignKey(nameof(Customer))]
        [Required(ErrorMessage = "Customer id is a required field.")]
        public Guid CustomerID { get; set; }

        [Required(ErrorMessage = "Order date is a required field.")]
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(DeliveryMethod))]
        [Required(ErrorMessage = "Delivery method id is a required field.")]
        public Guid DeliveryMethodID { get; set; }

        [ForeignKey(nameof(PaymentMethod))]
        [Required(ErrorMessage = "Payment method id is a required field.")]
        public Guid PaymentMethodID { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        [Required(ErrorMessage = "Order status is a required field.")]
        public Guid OrderStatusID { get; set; }

        [Required(ErrorMessage = "Comment is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the comment is 250 characters.")]
        public string Comment { get; set; }

        public Customer Customer { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
