namespace Tanka.Markdown.Blocks
{
    using Inline;

    public class ParagraphBuilder : IBlockBuilder
    {
        private readonly InlineParser _inlineParser;

        public ParagraphBuilder()
        {
            _inlineParser = new InlineParser();
        }

        public InlineParser InlineParser
        {
            get { return _inlineParser; }
        }

        public bool CanBuild(int start, StringRange content)
        {
            return true;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            end = start;
            return null;
        }

        public Paragraph Build(int start, int end, StringRange content)
        {
            var spans = _inlineParser.Parse(new StringRange(content, start, end));

            return new Paragraph(content, start, end, spans);
        }
    }
}