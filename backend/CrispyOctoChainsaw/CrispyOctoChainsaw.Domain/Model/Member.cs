namespace CrispyOctoChainsaw.Domain.Model
{
    public class Member
    {
        public int UserId { get; }

        public int CourseId { get; }

        public string? RepositoryLink { get; }

        public MemberStatus MemberStatus { get; }
    }
}
