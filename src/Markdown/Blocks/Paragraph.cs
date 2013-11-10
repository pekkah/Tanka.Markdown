namespace Tanka.Markdown.Blocks
{
    using System.Text;

    public class Paragraph : Block
    {
        public Paragraph(string content)
        {
            Content = content;
        }

        public string Content { get; private set; }
    }

    public class ParagraphBuilder : BlockBuilderBase
    {
        private readonly StringBuilder _builder;

        public ParagraphBuilder()
        {
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

            _builder.Append(line);
        }

        public override Block Create()
        {
            return new Paragraph(_builder.ToString());
        }
    }
}