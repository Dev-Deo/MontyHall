using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public class ApplicationUserCreateDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int? ContactNo { get; set; }
        public int TotalAttempt { get; set; }
    }
    //public class ApplicationUserWithoutBRCreateDto
    //{
    //    public string Email { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    [RegularExpression("(?=^.{8,15}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 8-15 characters")]
    //    public string Password { get; set; }
    //}
}
