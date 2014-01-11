namespace Tanka.Markdown.Text
{
    public static class TokenType
    {
        /// <summary>
        /// </summary>
        public const string Text = "(any)";

        /// <summary>
        ///     [
        /// </summary>
        public const string LinkTitleStart = "[";

        /// <summary>
        ///     ]
        /// </summary>
        public const string LinkTitleEnd = "]";

        /// <summary>
        ///     (
        /// </summary>
        public const string LinkUrlStart = "(";

        /// <summary>
        ///     )
        /// </summary>
        public const string LinkUrlEnd = ")";

        /// <summary>
        ///     *
        /// </summary>
        public const string Emphasis = "*";

        /// <summary>
        ///     End of string
        /// </summary>
        public const string End = "(eof)";

        /// <summary>
        /// !
        /// </summary>
        public const string Image = "!";

        public const string Unknown = "(unk)";

        public const string StrongEmphasis = "**";
    }
}