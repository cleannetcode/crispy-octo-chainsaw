using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for get course admin courses
    public class GetCourseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RepositoryName { get; }

        public string BannerName { get; set; }

        public Exercise[] Exercises { get; set; }
    }
}
