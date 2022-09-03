namespace CrispyOctoChainsaw.Domain.Model
{
    public class MemberExercise
    {
        public int UserId { get; }

        public int ExerciseId { get; }

        public string? PullRequestLink { get; }

        public string? BranchLink { get; }

        public MemberExerciseStatus ExerciseStatus { get; }
    }
}
