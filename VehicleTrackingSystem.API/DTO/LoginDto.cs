using System.ComponentModel.DataAnnotations;

namespace VehicleTrackingSystem.API.DTO
{
    public class LoginDto
    {

        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }

}
