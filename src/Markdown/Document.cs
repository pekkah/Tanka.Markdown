namespace Tanka.Markdown
{
    using System.Collections.Generic;

    public class Document
    {
        private readonly IEnumerable<Block> _blocks;

        public Document(IEnumerable<Block> blocks)
        {
            _blocks = blocks;
        }

        public IEnumerable<Block> Blocks
        {
            get { return _blocks; }
        }
    }
}