namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Inline;

    public class List : Block
    {
        public List(
            StringRange parent,
            int start,
            int end,
            bool isOrdered,
            IEnumerable<Item> items)
            : base(parent, start, end)
        {
            Items = items;
            IsOrdered = isOrdered;
        }

        public IEnumerable<Item> Items { get; private set; }
        public bool IsOrdered { get; private set; }
    }

    public class Item : Paragraph
    {
        public Item(StringRange parent, int start, int end, IEnumerable<Span> spans) : base(parent, start, end, spans)
        {
        }
    }
}