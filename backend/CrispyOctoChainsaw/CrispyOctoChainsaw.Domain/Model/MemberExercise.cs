namespace CrispyOctoChainsaw.Domain.Model
{
    public class MemberExercise
    {
        public long UserId { get; }

        public long ExerciseId { get; }

        public string? PullRequestLink { get; }

        public string? BranchLink { get; }

        Status Status { get; }
    }
}
