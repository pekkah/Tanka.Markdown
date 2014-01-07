namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;

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

        public IEnumerable<LinkDefinition> LinkDefinitions
        {
            get
            {
                return _blocks.OfType<LinkDefinition>();
            }
        } 
    }
}