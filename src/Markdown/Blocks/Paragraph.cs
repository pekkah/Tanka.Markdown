namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Text;

    public class Paragraph : Block
    {
        private readonly IEnumerable<ISpan> _content;

        public Paragraph(IEnumerable<ISpan> content)
        {
            _content = content;
        }

        public IEnumerable<ISpan> Content
        {
            get { return _content; }
        }
    }
}