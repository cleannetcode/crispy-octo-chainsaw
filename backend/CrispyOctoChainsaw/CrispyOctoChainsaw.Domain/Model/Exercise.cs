namespace CrispyOctoChainsaw.Domain.Model
{
    public record Exercise
    {
        public long Id { get; }
        public string? Title { get; }
        public string? Description { get; }
        public string? BranchName { get; }
    }
}
