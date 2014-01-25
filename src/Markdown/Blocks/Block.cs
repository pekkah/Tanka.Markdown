namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public abstract class Block : StringRange
    {
        protected Block(StringRange parent, int start, int end) : base(parent, start, end)
        {
        }
    }
}