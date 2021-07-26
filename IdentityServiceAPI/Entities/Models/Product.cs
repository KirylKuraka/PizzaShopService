using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Product
    {
        [Column("ProductID")]
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Cost is a required field.")]
        [Range(0, 9999)]
        public float Cost { get; set; }

        [Required(ErrorMessage = "Promotional points cost is a required field.")]
        [Range(0, 9999)]
        public float PromotionalPointsCost { get; set; }

        [ForeignKey(nameof(ProductType))]
        public Guid ProductTypeID { get; set; }

        public ProductType ProductType { get; set; }
    }
}
