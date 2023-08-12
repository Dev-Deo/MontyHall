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
    }

}
