using CrispyOctoChainsaw.Domain;

namespace CrispyOctoChainsaw.UnitTests.MemberData
{
    public class CourseAdminDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidGuidString(int testCount)
        {
            var rnd = new Random();

            for (int i = 0; i < testCount; i++)
            {
                var invalidStringLength = rnd.Next(
                    CourseAdmin.MAX_LENGTH_NICKNAME + 1,
                    int.MaxValue / 1000);

                var invalidStrings = Enumerable.Range(0, 5)
                    .Select(x => StringFixture.GenerateRandomString(invalidStringLength))
                    .ToArray();

                var invalidString = BaseDataGenerator.MakeInvalidString(invalidStrings);

                yield return new object[]
                {
                    Guid.Empty, invalidString
                };
            }
        }
    }
}
