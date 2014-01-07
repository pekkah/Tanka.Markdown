namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Text;
    using Text;

    public class ParagraphBuilder : BlockBuilderBase
    {
        private readonly StringBuilder _builder;
        private readonly TextSpanParser _textParser;

        public ParagraphBuilder(IEnumerable<SpanFactoryBase> factories, StringTokenizer tokenizer)
        {
            _textParser = new TextSpanParser(factories, tokenizer);
            _builder = new StringBuilder();
        }

        public ParagraphBuilder()
        {
            _textParser = new TextSpanParser();
            _builder = new StringBuilder();
        }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(nextLine))
            {
                return true;
            }

            return false;
        }

        public override bool End()
        {
            return true;
        }

        public override void AddLine(string currentLine)
        {
            string line = currentLine.Trim(' ', '\r', '\n');

            _builder.Append(string.Concat(line, " "));
        }

        public override Block Create()
        {
            IEnumerable<ISpan> spans = _textParser.Parse(_builder.ToString().Trim());
            return new Paragraph(spans);
        }
    }
}