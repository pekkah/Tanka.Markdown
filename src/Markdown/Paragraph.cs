namespace Tanka.Markdown
{
    using System.Text;

    public class Paragraph : Block
    {
        private readonly StringBuilder _builder;

        public Paragraph()
        {
            _builder = new StringBuilder();
        }

        public string Content
        {
            get { return _builder.ToString(); }
        }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(nextLine))
            {
                return true;
            }

            return false;
        }

        public override bool End(string currentLine)
        {
            // last line before the empty or null line
            AddLine(currentLine);

            return true;
        }

        public override void AddLine(string currentLine)
        {
            var line = currentLine.Trim(' ', '\r', '\n');

            _builder.Append(line);
        }
    }
}