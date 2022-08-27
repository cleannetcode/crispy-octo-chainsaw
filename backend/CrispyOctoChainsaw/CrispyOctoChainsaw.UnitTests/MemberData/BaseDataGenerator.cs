namespace CrispyOctoChainsaw.UnitTests.MemberData
{
    public class BaseDataGenerator
    {
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
    }
}
