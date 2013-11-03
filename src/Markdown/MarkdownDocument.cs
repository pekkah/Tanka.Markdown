namespace Tanka.Markdown
{
    using System.Collections.Generic;

    public class MarkdownDocument
    {
        private readonly IEnumerable<Block> _blocks;

        public MarkdownDocument(IEnumerable<Block> blocks)
        {
            _blocks = blocks;
        }

        public IEnumerable<Block> Blocks
        {
            get
            {
                return _blocks;
            }
        }
    }
}