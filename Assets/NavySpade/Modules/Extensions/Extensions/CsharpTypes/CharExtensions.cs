namespace Core.Extensions.CsharpTypes
{
    public static class CharExtensions
    {
        public static bool IsWhitespace(this char value)
            => char.IsWhiteSpace(value);

        public static bool IsWhitespaceOrNonBreakingSpace(this char value)
            => value.IsWhitespace() || value == '\u200B';

        public static bool EqualsCaseInsensitive(this char value, char other)
            => char.ToUpperInvariant(value) == char.ToUpperInvariant(other);
    }
}