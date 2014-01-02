namespace Tanka.Markdown.Text
{
    public enum TokenType
    {
        /// <summary>
        /// </summary>
        Text,

        /// <summary>
        ///     [
        /// </summary>
        LinkTitleStart,

        /// <summary>
        ///     ]
        /// </summary>
        LinkTitleEnd,

        /// <summary>
        ///     (
        /// </summary>
        LinkUrlStart,

        /// <summary>
        ///     )
        /// </summary>
        LinkUrlEnd,

        /// <summary>
        ///     *
        /// </summary>
        Emphasis,

        /// <summary>
        ///     End of string
        /// </summary>
        End,

        /// <summary>
        /// !
        /// </summary>
        Image
    }
}