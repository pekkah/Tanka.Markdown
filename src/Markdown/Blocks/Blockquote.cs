namespace Tanka.Markdown.Blocks
{
    using System.Text;

    public class Blockquote : Block
    {
        public Blockquote(Document document)
        {
            Document = document;
        }

        public Document Document { get; protected set; }
    }

    public class BlockquoteBuilder : BlockBuilderBase
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

        public override bool End()
        {
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