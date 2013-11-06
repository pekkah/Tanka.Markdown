namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListBlock : Block
    {
        private readonly IEnumerable<string> _items;

        public int Count
        {
            get { return _items.Count(); }
        }

        public IEnumerable<string> Items
        {
            get
            {
                return _items;
            }
        }

        public ListBlock(IEnumerable<string> items)
        {
            _items = items;
        }
    }

    public class ListBlockBuilder : BlockBuilder
    {
        private readonly List<string> _items;
        private StringBuilder _currentItemBuilder;

        public ListBlockBuilder()
        {
            _items = new List<string>();
        }

        

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(nextLine))
                return true;

            return false;
        }

        public override bool End(string currentLine)
        {
            AddLine(currentLine);

            // finish hanging item
            _items.Add(_currentItemBuilder.ToString());

            return true;
        }

        public override void AddLine(string currentLine)
        {
            // new item starts with a list item marker
            if (ListBlockFactory.IsListItem.Any(f => f(currentLine)))
            {
                // end previous item if item exists
                if (_currentItemBuilder != null)
                {
                    _items.Add(_currentItemBuilder.ToString());
                }

                _currentItemBuilder = new StringBuilder();
            }

            _currentItemBuilder.Append(Clean(currentLine));
        }

        public override Block Create()
        {
            return new ListBlock(_items);
        }

        private string Clean(string line)
        {
            if (line.StartsWith("*"))
                return line.Substring(1).Trim();

            if (line.StartsWith("-"))
                return line.Substring(1).Trim();

            var possiblyANumberEnd = line.IndexOf('.');
            if (possiblyANumberEnd > -1)
            {
                var possiblyANumber = line.Substring(0, possiblyANumberEnd);
                int _ = 0;

                if (int.TryParse(possiblyANumber, out _))
                    return line.Substring(possiblyANumberEnd + 1).Trim();
            }

            return line.Trim();
        }
    }
}