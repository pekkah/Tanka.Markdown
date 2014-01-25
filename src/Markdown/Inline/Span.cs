namespace Tanka.Markdown.Inline
{
    public abstract class Span : StringRange
    {
        protected Span(StringRange parentRange, int start, int end)
            : base(parentRange.Document, start, end)
        {
        }
    }
}