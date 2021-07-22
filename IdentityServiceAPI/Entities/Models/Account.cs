using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Account
    {
        [Column("Id")]
        [Key]
        public Guid UserID { get; set; }

        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }

        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email name is a required field.")]
        [EmailAddress]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Email { get; set; }

        [Phone]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Promotion points is a required field.")]
        [Range(0, 9999)]
        public float PromotionalPoins { get; set; } = 0F;
    }
}
