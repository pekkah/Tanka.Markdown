namespace Tanka.Markdown
{
    using System.Text;

    public class Blockquote: Block
    {
        public Blockquote(MarkdownDocument document)
        {
            Document = document;
        }

        public MarkdownDocument Document { get; protected set; }
    }

    public class BlockquoteBuilder : BlockBuilder
    {
        private readonly StringBuilder _documentBuilder;

        public BlockquoteBuilder()
        {
            _documentBuilder = new StringBuilder();
        }


        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(nextLine))
                return true;

            if (nextLine.StartsWith("> "))
                return false;

            return true;
        }

        public override bool End(string currentLine)
        {
            AddLine(currentLine);

            return false;
        }

        public override void AddLine(string currentLine)
        {
            // trim '>' and whitespace from lines
            _documentBuilder.AppendLine(currentLine.TrimStart('>').Trim());
        }

        public override Block Create()
        {
            var parser = new MarkdownParser();
            return new Blockquote(parser.Parse(_documentBuilder.ToString()));
        }
    }
}