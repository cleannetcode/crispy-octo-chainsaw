namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for edit course
    public class EditCourseRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string RepositoryName { get; set; }
    }
}
