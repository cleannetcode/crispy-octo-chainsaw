namespace CrispyOctoChainsaw.IntegrationalTests.MemberData
{
    public class CmsCoursesDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidTitleDescriptionRepositotyName(
            int testCount)
        {
            var rnd = new Random();

            for (int i = 0; i < testCount; i++)
            {
                var titleLength = BaseDataGenerator.GetInvalidLengthCourseTitle();
                var descriptionLength = BaseDataGenerator.GetInvalidLengthCourseDescription();
                var repositoryNameLength = BaseDataGenerator.GetInvalidLengthCourseRepositoryName();

                var titles = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleLength))
                    .ToArray();

                var descriptions = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionLength))
                    .ToArray();

                var repositoryNames = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(repositoryNameLength))
                    .ToArray();

                var title = BaseDataGenerator.MakeInvalidStringWithoutNull(titles);
                var description = BaseDataGenerator.MakeInvalidStringWithoutNull(descriptions);
                var repositoryName = BaseDataGenerator.MakeInvalidStringWithoutNull(repositoryNames);

                yield return new object[]
                {
                    title, description, repositoryName, Array.Empty<byte[]>()
                };
            }
        }

        public static IEnumerable<object[]> GenerateSetInvalidCourseId(int countTest)
        {
            for (int i = 0; i < countTest; i++)
            {
                var cardId = BaseDataGenerator.MakeInvalidNumber();

                yield return new object[]
                {
                   cardId
                };
            }
        }
    }
}
