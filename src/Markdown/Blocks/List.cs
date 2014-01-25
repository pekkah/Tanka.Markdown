namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;

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

    public class Item : List
    {
        public Item(
            StringRange parent,
            int start,
            int end,
            bool isOrdered,
            IEnumerable<Item> items = null)
            : base(parent, start, end, isOrdered, null)
        {
        }
    }
}