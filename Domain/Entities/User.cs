using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{

    public class User
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } // Full Name

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } // Phone Number

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email

        [Required]
        public string PasswordHash { get; set; } // Hashed Password

        [Required]
        public bool IsPlayer { get; set; } // User Type
    }

}
