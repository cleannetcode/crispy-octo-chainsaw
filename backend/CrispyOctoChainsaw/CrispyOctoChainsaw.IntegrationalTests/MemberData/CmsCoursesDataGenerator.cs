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
                var titleLength = BaseDataGenerator.GetInvalidLengthTitle();
                var descriptionLength = BaseDataGenerator.GetInvalidLengthDescription();
                var repositoryNameLength = BaseDataGenerator.GetInvalidLengthRepositoryName();

                var titles = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleLength))
                    .ToArray();

                var descriptions = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionLength))
                    .ToArray();

                var repositoryNames = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(repositoryNameLength))
                    .ToArray();

                var title = BaseDataGenerator.MakeInvalidString(titles);
                var description = BaseDataGenerator.MakeInvalidString(descriptions);
                var repositoryName = BaseDataGenerator.MakeInvalidString(repositoryNames);

                yield return new object[]
                {
                    title, description, repositoryName
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
