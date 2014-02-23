namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Inline;

    public class Paragraph : Block
    {
        private readonly List<Span> _spans;

        public Paragraph(StringRange parent, int start, int end, IEnumerable<Span> spans) : base(parent, start, end)
        {
            _spans = new List<Span>(spans);
        }

        public IEnumerable<Span> Spans
        {
            get { return _spans; }
        }

        public void Replace(Span target, LinkSpan withThis)
        {
            int indexOf = _spans.IndexOf(target);
            _spans.Remove(target);
            _spans.Insert(indexOf, withThis);
        }

        public bool IsEmpty()
        {
            if (!_spans.Any())
                return true;

            if (_spans.Count() != 1)
            {
                return false;
            }

            var isNewLine = _spans.OfType<NewLineSpan>().Any();

            return isNewLine;
        }
    }
}