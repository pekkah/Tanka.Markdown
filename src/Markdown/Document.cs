namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using Blocks;

    public class Document
    {
        public Document(IEnumerable<Block> blocks, string markdown)
        {
            Blocks = blocks;
            Markdown = markdown;
        }
        public IEnumerable<Block> Blocks { get; private set; }
        public string Markdown { get; private set; }
    }
}