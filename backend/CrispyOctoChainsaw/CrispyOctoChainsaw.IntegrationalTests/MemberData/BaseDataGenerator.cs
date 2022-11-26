using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.IntegrationalTests.MemberData
{
    public class BaseDataGenerator
    {
        public static int GetInvalidLengthCourseTitle()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxTitleLength + 1,
                    Course.MaxTitleLength + 4);

            return length;
        }

        public static int GetInvalidLengthExerciseTitle()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Exercise.MaxTitleLength + 1,
                    Exercise.MaxTitleLength + 4);

            return length;
        }

        public static int GetInvalidLengthCourseDescription()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxDescriptionsLength + 1,
                    Course.MaxDescriptionsLength + 4);

            return length;
        }

        public static int GetInvalidLengthExerciseDescription()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Exercise.MaxDescriptionsLength + 1,
                    Exercise.MaxDescriptionsLength + 4);

            return length;
        }

        public static int GetInvalidLengthCourseRepositoryName()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxRepositoryNameLength + 1,
                    Course.MaxRepositoryNameLength + 4);

            return length;
        }

        public static int GetInvalidLengthExerciseBranchName()
        {
            var rnd = new Random();

            var length = rnd.Next(
                    Course.MaxRepositoryNameLength + 1,
                    Course.MaxRepositoryNameLength + 4);

            return length;
        }

        public static string MakeInvalidStringWithoutNull(params string[] invalidData)
        {
            var rnd = new Random();

            var data = new List<string>
            {
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

            var number = -rnd.Next(0, 100);

            return number;
        }
    }
}
