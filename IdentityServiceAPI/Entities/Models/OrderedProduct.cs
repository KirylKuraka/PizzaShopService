using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class OrderedProduct
    {
        [Column("OrderedProductID")]
        [Key]
        public Guid OrderedProductID { get; set; }

        [ForeignKey(nameof(ProductType))]
        [Required(ErrorMessage = "Product type id is a required field.")]
        public Guid ProductTypeID { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        [Range(0, 9999)]
        public int Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        [Required(ErrorMessage = "Order id is a required field.")]
        public Guid OrderID { get; set; }

        [Required(ErrorMessage = "Total cost is a required field.")]
        [Range(0, 9999)]
        public float TotalCost { get; set; }

        public ProductType ProductType { get; set; }
        public Order Order { get; set; }
    }
}
