namespace Tanka.Markdown.Inline
{
    public class ImageSpan : Span
    {
        public ImageSpan(StringRange parent, int start, int end, StringRange title, StringRange url)
            : base(parent, start, end)
        {
            Title = title;
            Url = url;
        }

        public StringRange Url { get; private set; }

        public StringRange Title { get; private set; }
    }
}