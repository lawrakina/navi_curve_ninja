namespace NavySpade.Modules.Extensions.CsharpTypes
{
    /// <summary>
    /// <see cref="char"/> extensions.
    /// </summary>
    public static class CharExtensions
    {
        #region Syntax

        public static bool IsWhitespace(this char value)
            => char.IsWhiteSpace(value);

        public static bool IsWhitespaceOrNonBreakingSpace(this char value)
            => value.IsWhitespace() || value == '\u200B';

        public static bool EqualsCaseInsensitive(this char value, char other)
            => char.ToUpperInvariant(value) == char.ToUpperInvariant(other);

        #endregion
    }
}