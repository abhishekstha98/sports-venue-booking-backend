﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email or Phone is required.")]
        public string EmailOrPhone { get; set; } // Accepts either email or phone number

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } // User's password
    }
}
