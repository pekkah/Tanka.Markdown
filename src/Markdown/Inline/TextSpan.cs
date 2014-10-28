namespace Tanka.Markdown.Inline
{
    public class TextSpan : Span
    {
        public TextSpan(StringRange parent, int start, int end, bool cleanInput = true)
            : base(parent, start, end, cleanInput)
        {
        }
    }
}