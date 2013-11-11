namespace Tanka.Markdown.Blocks
{
    using System;
    using System.Text;

    public class CodeblockBuilder : BlockBuilderBase
    {
        private readonly StringBuilder _codeBuilder;
        private string _language;

        public CodeblockBuilder()
        {
            _codeBuilder = new StringBuilder();
        }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(nextLine))
                return false;

            if (nextLine.StartsWith("```"))
                return true;

            return false;
        }

        public override bool End()
        {
            // endline is found when next line has end marker so skip required
            return true;
        }

        public override void AddLine(string currentLine)
        {
            // first line with the language
            if (currentLine.StartsWith("```"))
            {
                int firstIndexOfSpace = currentLine.IndexOf(" ", StringComparison.Ordinal);

                if (firstIndexOfSpace > 0)
                {
                    _language = currentLine.Substring(firstIndexOfSpace).Trim();
                    return;
                }

                // end line
                return;
            }

            _codeBuilder.Append(currentLine);
        }

        public override Block Create()
        {
            return new Codeblock(_language, _codeBuilder.ToString());
        }
    }
}