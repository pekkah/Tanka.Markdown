namespace Tanka.Markdown.Blocks
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;
    using Markdown;

    public class EmptyLineBuilder : IBlockBuilder
    {
        private readonly Regex _expression;

        public EmptyLineBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .LineBreak()
                .LineBreak()
                .ToRegex();
        }

        public bool CanBuild(int start, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, start);

            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            // \r\n\r\n or \n\n
            end = content.EndOfLine(start + 2, true);
            return new EmptyLine(content, start, end);
        }
    }
}
