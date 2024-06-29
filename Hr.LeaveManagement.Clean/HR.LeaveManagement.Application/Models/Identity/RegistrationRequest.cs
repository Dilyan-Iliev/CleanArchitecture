using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set;} = null!;

        [Required]
        [MinLength(6)]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;    
    }
}
