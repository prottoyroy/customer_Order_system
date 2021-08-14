using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
      //  [Required]
       [Required] 
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
       [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        public string Password { get; set; }

        [Required] 
        [DataType(DataType.Password)]
        [Compare("Password")]
         
        public string ConfirmPassword{get;set;}
    }

    public class LogInModel
    {
       // public string UserName { get; set;}
        public string Email { get; set; }
       [Required] 
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
       [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        public string Password { get; set; }

        [Required] 
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword{get;set;}

    }
    
}