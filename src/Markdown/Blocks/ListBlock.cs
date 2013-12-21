namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Linq;

    public class ListBlock : Block
    {
        private readonly IEnumerable<string> _items;

        public ListBlock(IEnumerable<string> items)
        {
            _items = items;
        }

        public int Count
        {
            get { return _items.Count(); }
        }

        public IEnumerable<string> Items
        {
            get { return _items; }
        }

        public ListStyle Style { get; set; }
    }
}