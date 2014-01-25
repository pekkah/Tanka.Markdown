namespace Tanka.Markdown.Inline
{
    public class Emphasis : Span
    {
        public Emphasis(StringRange parent, int start) : base(parent, start, start)
        {
        }
    }

    public class StrongEmphasis : Span
    {
        public StrongEmphasis(StringRange parent, int start) : base(parent, start, start + 1)
        {
        }
    }
}