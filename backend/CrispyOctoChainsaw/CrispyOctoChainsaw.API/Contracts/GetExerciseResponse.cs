namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for get exercise
    public class GetExerciseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BranchName { get; set; }

        public int CourseId { get; set; }
    }
}
