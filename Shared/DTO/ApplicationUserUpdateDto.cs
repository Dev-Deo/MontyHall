namespace Shared.DTO
{
    public class ApplicationUserUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ContactNo { get; set; }
    }
}
