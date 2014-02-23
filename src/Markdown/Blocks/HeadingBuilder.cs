namespace Tanka.Markdown.Blocks
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;
    using Markdown;

    public class HeadingBuilder : IBlockBuilder
    {
        private readonly Regex _expression;

        public HeadingBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .StartOfLine()
                .Any("#")
                .Anything()
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
            end = content.EndOfLine(start, false);
 
            var counter = start;
            var level = 0;
            while (content[counter] == '#')
            {
                level++;
                counter++;
            }

            return new Heading(content, counter + 1, end, level);
        }
    }
}
