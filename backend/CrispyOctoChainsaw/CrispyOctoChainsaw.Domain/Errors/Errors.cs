namespace CrispyOctoChainsaw.Domain.Errors
{
    public static class Errors
    {
        public static class Title
        {
            public const string TitleCanNotBeNullOrWhiteSpace = "Title не может быть пустым или null";
            public const string TitleMaxLength = "Title не может быть больше {0} символов";
        }

        public static class Descriptions
        {
            public const string DescriptionsCanNotBeNullOrWhiteSpace = "Descriptions не может быть пустым или null";
            public const string DescriptionsMaxLength = "Descriptions не может быть больше {1} символов";
        }
    }
}
