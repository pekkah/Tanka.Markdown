namespace Tanka.Markdown
{
    using System.Text;

    public class Blockquote : Block
    {
        private readonly StringBuilder _documentBuilder;

        public Blockquote()
        {
            _documentBuilder = new StringBuilder();
        }

        public MarkdownDocument Document { get; protected set; }

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

            ParseDocument();

            return false;
        }

        private void ParseDocument()
        {
            var parser = new MarkdownParser();

            Document = parser.Parse(_documentBuilder.ToString());
        }

        public override void AddLine(string currentLine)
        {
            // trim '>' and whitespace from lines
            _documentBuilder.AppendLine(currentLine.TrimStart('>').Trim());
        }
    }
}