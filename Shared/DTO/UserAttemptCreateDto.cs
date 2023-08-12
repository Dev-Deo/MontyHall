namespace Shared.DTO
{
    public class UserAttemptCreateDto
    {
        public Guid UserId { get; set; }
        public int TotalAttempt { get; set; }
    }
}
