using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.UnitTests.MemberData
{
    public class CourseDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidAllParameters(int testCount)
        {
            var rnd = new Random();
            for (int i = 0; i < testCount; i++)
            {
                // title
                var titleInvalidLength = rnd.Next(Course.MaxTitleLength, Course.MaxTitleLength + 4);

                var invalidTitleData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleInvalidLength))
                    .ToArray();

                var invalidTitle = BaseDataGenerator.MakeInvalidString(invalidTitleData);

                // description
                var descriptionInvalidLength = rnd.Next(
                    Course.MaxDescriptionsLength,
                    Course.MaxDescriptionsLength + 4);

                var invalidDescriptionData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionInvalidLength))
                    .ToArray();

                var invalidDescription = BaseDataGenerator.MakeInvalidString(invalidDescriptionData);

                // repositoryName
                var repositoryNameInvalidLength = rnd.Next(
                    Course.MaxRepositoryNameLength,
                    Course.MaxRepositoryNameLength + 4);

                var invalidRepositoryNameData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(repositoryNameInvalidLength))
                    .ToArray();

                var invalidRepositoryName = BaseDataGenerator.MakeInvalidString(invalidRepositoryNameData);

                // imageName
                var imageNameInvalidLength = rnd.Next(
                    Course.MaxBannerNameLength,
                    Course.MaxBannerNameLength + 4);

                var invalidImageNameData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(imageNameInvalidLength))
                    .ToArray();

                var invalidImageName = BaseDataGenerator.MakeInvalidString(invalidImageNameData);

                yield return new object[]
                {
                    invalidTitle,
                    invalidDescription,
                    invalidRepositoryName,
                    Guid.Empty,
                    invalidImageName
                };
            }
        }
    }
}
