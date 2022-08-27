namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for create course
    public class CreateCourseRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string RepositoryName { get; set; }
    }
}
