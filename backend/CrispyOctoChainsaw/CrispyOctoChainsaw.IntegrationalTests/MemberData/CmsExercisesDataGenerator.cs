namespace CrispyOctoChainsaw.IntegrationalTests.MemberData
{
    public class CmsExercisesDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidTitleDescriptionBranchName(
            int testCount)
        {
            var rnd = new Random();

            for (int i = 0; i < testCount; i++)
            {
                var titleLength = BaseDataGenerator.GetInvalidLengthExerciseTitle();
                var descriptionLength = BaseDataGenerator.GetInvalidLengthExerciseDescription();
                var branchNameLength = BaseDataGenerator.GetInvalidLengthExerciseBranchName();

                var titles = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleLength))
                    .ToArray();

                var descriptions = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionLength))
                    .ToArray();

                var branchNames = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(branchNameLength))
                    .ToArray();

                var title = BaseDataGenerator.MakeInvalidString(titles);
                var description = BaseDataGenerator.MakeInvalidString(descriptions);
                var branchName = BaseDataGenerator.MakeInvalidString(branchNames);

                yield return new object[]
                {
                    title, description, branchName
                };
            }
        }

        public static IEnumerable<object[]> GenerateSetInvalidTitleDescriptionBranchNameCourseId(
            int testCount)
        {
            var rnd = new Random();

            for (int i = 0; i < testCount; i++)
            {
                var titleLength = BaseDataGenerator.GetInvalidLengthExerciseTitle();
                var descriptionLength = BaseDataGenerator.GetInvalidLengthExerciseDescription();
                var branchNameLength = BaseDataGenerator.GetInvalidLengthExerciseBranchName();

                var titles = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleLength))
                    .ToArray();

                var descriptions = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionLength))
                    .ToArray();

                var branchNames = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(branchNameLength))
                    .ToArray();

                var title = BaseDataGenerator.MakeInvalidString(titles);
                var description = BaseDataGenerator.MakeInvalidString(descriptions);
                var branchName = BaseDataGenerator.MakeInvalidString(branchNames);
                var courseId = BaseDataGenerator.MakeInvalidNumber();

                yield return new object[]
                {
                    title, description, branchName, courseId
                };
            }
        }

        public static IEnumerable<object[]> GenerateSetInvalidId(int countTest)
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
