namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListBlock : Block
    {
        private readonly List<string> _items;
        private StringBuilder _currentItemBuilder;

        public ListBlock()
        {
            _items = new List<string>();
        }

        public int Count
        {
            get { return _items.Count; }
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

            _currentItemBuilder.AppendLine(currentLine);
        }
    }
}