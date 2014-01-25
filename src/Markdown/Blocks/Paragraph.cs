namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Inline;

    public class Paragraph : Block
    {
        private readonly List<Span> _spans;

        public Paragraph(
            StringRange parent,
            int start,
            int end) : base(parent, start, end)
        {
            _spans = ParseSpans();
        }

        public IEnumerable<Span> Spans
        {
            get
            {
                return _spans;
            }
        }

        private List<Span> ParseSpans()
        {
            var parser = new InlineParser();
            return parser.Parse(this).ToList();
        }

        public void Replace(Span target, LinkSpan withThis)
        {
            var indexOf = _spans.IndexOf(target);
            _spans.Remove(target);
            _spans.Insert(indexOf, withThis);
        }
    }
}