namespace Tanka.Markdown.Inline
{
    public class ReferenceLinkSpan : Span
    {
        public ReferenceLinkSpan(StringRange parent, int start, int end, StringRange title, StringRange key)
            : base(parent, start, end)
        {
            Title = title;
            Key = key;
        }

        public StringRange Key { get; private set; }

        public StringRange Title { get; private set; }
    }
}