using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.IntegrationalTests.MemberData
{
    public class BaseDataGenerator
    {
        public static int GetInvalidLengthTitle()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxTitleLength + 1,
                    int.MaxValue / 1000);

            return length;
        }

        public static int GetInvalidLengthDescription()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxDescriptionsLength + 1,
                    int.MaxValue / 1000);

            return length;
        }

        public static int GetInvalidLengthRepositoryName()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxRepositoryNameLength + 1,
                    int.MaxValue / 1000);

            return length;
        }

        public static string MakeInvalidString(params string[] invalidData)
        {
            var rnd = new Random();

            var data = new List<string>
            {
                null,
                string.Empty
            };

            data.AddRange(invalidData ?? Array.Empty<string>());

            var whiteSpace = Enumerable.Range(0, 5)
                .Select(x => string.Empty.PadLeft(rnd.Next(1, 100)))
                .ToArray();

            data.AddRange(whiteSpace);

            var invalidString = data[rnd.Next(0, data.Count)];

            return invalidString;
        }

        public static int MakeInvalidNumber()
        {
            var rnd = new Random();

            var number = -rnd.Next(0, int.MaxValue);

            return number;
        }
    }
}
