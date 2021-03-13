using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.API.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Password { get; set; }
    }
}
