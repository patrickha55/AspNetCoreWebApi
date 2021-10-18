using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs
{
    public class UserDTO : SignInDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [DataType(DataType.PhoneNumber)] 
        public string PhoneNumber { get; set; }
    }

    public class SignInDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterDTO : SignInDTO
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [DataType(DataType.PhoneNumber)] 
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}