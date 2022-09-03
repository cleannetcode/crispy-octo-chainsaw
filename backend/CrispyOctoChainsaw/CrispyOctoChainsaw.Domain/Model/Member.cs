namespace CrispyOctoChainsaw.Domain.Model
{
    public class Member
    {
        public long UserId { get; }

        public long CourseId { get; }

        public string? RepositoryLink { get; }

        Status Status { get; }
    }
}
