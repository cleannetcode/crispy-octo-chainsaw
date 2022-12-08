namespace CrispyOctoChainsaw.IntegrationalTests.MemberData
{
    public class CoursesControllerDataGenerator
    {
        public static IEnumerable<object[]> GenerateSetInvalidId(int testCount)
        {
            for (int i = 0; i < testCount; i++)
            {
                var id = BaseDataGenerator.MakeInvalidNumber();
                yield return new object[]
                {
                    id
                };
            }
        }
    }
}
