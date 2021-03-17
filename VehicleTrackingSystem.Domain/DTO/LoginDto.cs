using System.ComponentModel.DataAnnotations;

namespace VehicleTrackingSystem.Domain.DTO
{
    public class LoginDto
    {

        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }

}
