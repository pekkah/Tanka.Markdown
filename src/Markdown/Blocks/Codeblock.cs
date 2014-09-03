using System.Text.RegularExpressions;

namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class Codeblock : Block
    {
        private readonly StringRange parent;

        public string Syntax
        {
            get
            {
                var match = SyntaxIdentifieRegex.Match(parent.Document);
                return match.Success ? match.Groups[1].Value : null;
            }
        }

        private static readonly Regex SyntaxIdentifieRegex = new Regex("```(.*)\r");
        public Codeblock(StringRange parent, int start, int end) : base(parent, start, end)
        {
            this.parent = parent;
        }
    }
}