using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class ProductType
    {
        [Column("ProductTypeID")]
        public Guid ProductTypeID { get; set; }

        [Required(ErrorMessage = "Product type name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string ProductTypeName { get; set; }
    }
}
