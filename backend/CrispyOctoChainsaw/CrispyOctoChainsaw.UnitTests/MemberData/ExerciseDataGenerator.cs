using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.UnitTests.MemberData
{
    public class ExerciseDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidAllParameters(int testCount)
        {
            var rnd = new Random();
            for (int i = 0; i < testCount; i++)
            {
                // title
                var titleInvalidLength = rnd.Next(
                    Exercise.MaxTitleLength,
                    Exercise.MaxTitleLength + 4);

                var invalidTitleData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(titleInvalidLength))
                    .ToArray();

                var invalidTitle = BaseDataGenerator.MakeInvalidString(invalidTitleData);

                // description
                var descriptionInvalidLength = rnd.Next(
                    Exercise.MaxDescriptionsLength,
                    Exercise.MaxDescriptionsLength + 4);

                var invalidDescriptionData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(descriptionInvalidLength))
                    .ToArray();

                var invalidDescription = BaseDataGenerator.MakeInvalidString(invalidDescriptionData);

                // branchName
                var branchyNameInvalidLength = rnd.Next(
                    Exercise.MaxBranchNameLength,
                    Exercise.MaxBranchNameLength + 4);

                var invalidBranchNameData = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(branchyNameInvalidLength))
                    .ToArray();

                var invalidBranchName = BaseDataGenerator.MakeInvalidString(invalidBranchNameData);

                var invalidId = BaseDataGenerator.MakeInvalidId();

                yield return new object[]
                {
                    invalidTitle,
                    invalidDescription,
                    invalidBranchName,
                    invalidId
                };
            }
        }
    }
}
