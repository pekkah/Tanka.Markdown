namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class EmptyLine : Block
    {
        public EmptyLine(StringRange parent, int start, int end) : base(parent, start, end)
        {
        }
    }
}