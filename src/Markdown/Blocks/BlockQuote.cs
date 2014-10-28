namespace Tanka.Markdown.Blocks
{
    public class BlockQuote : Block
    {
        private readonly Block[] childBlocks;

        public BlockQuote(StringRange parent, int start, int end, params Block[] childBlocks) : base(parent, start, end)
        {
            this.childBlocks = childBlocks;
        }

        public Block[] ChildBlocks
        {
            get { return childBlocks; }
        }
    }
}
