namespace Tanka.Markdown.Blocks
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    /// <summary>
    /// Block-quotes can be interpreted as code blocks.
    /// 
    /// A code block is usually
    ///     > A code block
    /// </summary>
    public class BlockQuoteCodeBlockBuilder : IBlockBuilder
    {
        private Regex _expression;

        public BlockQuoteCodeBlockBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
               .Add(@"\G", false)
               .StartOfLine()
               .Find("    >")
               .Anything()
               .EndOfLine()
               .ToRegex();
        }

        public bool CanBuild(int start, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document,start);
            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            
            end = content.EndOfLine(start);
            var match = _expression.Match(content.Document, start);
            if (match.Success)
            {
                // code block can span multiple lines
                var unquotedString = MatchLines(content, start, out end);
                return new Codeblock(new StringRange(unquotedString,0,unquotedString.Length-1), 0, unquotedString.Length-1);
            }
            throw new ApplicationException("No match");
        }

        private string MatchLines(StringRange content,int start, out int end)
        {
            var sb = new StringBuilder();
            var match = _expression.Match(content.Document, start);
            end = start;
            if (match.Success)
            {
                var endOfLine = content.EndOfLine(start);
                if (endOfLine > 0) end = endOfLine;
                // advance 5
                start += 4;
                sb.AppendLine(content.Document.Substring(start, (end - start) + 1));
                var more = MatchLines(content, end + 3, out end);
                sb.Append(more);
            }
            return sb.ToString();
        }
    }
}
