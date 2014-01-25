namespace Tanka.Markdown.Inline
{
    public class TextSpan : Span
    {
        public TextSpan(StringRange parent, int start, int end)
            : base(parent, start, end)
        {
        }
    }
}