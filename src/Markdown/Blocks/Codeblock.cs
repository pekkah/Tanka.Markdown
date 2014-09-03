namespace Tanka.Markdown.Blocks
{
    public class Codeblock : Block
    {
        public Codeblock(StringRange parent, int start, int end) : base(parent, start, end)
        {
            Syntax = null;
        }

        public Codeblock(StringRange parent, int start, int end, StringRange syntax)
            : base(parent, start, end)
        {
            Syntax = syntax;
        }

        public StringRange Syntax { get; private set; }
    }
}