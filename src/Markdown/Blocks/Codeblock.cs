namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class Codeblock : Block
    {
        public Codeblock(StringRange parent, int start, int end) : base(parent, start, end)
        {
        }
    }
}