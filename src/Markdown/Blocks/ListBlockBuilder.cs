namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Text;

    public enum ListStyle
    {
        Ordered,
        Unordered
    }

    public class ListBlockBuilder : BlockBuilderBase
    {
        private readonly List<string> _items;
        private readonly ListStyle _style;
        private StringBuilder _currentItemBuilder;

        public ListBlockBuilder(ListStyle style)
        {
            _style = style;
            _items = new List<string>();
        }

        public ListStyle Style
        {
            get { return _style; }
        }

        private string GetItem(string currentLine)
        {
            if (string.IsNullOrEmpty(currentLine))
                return null;

            string item = null;
            switch (Style)
            {
                case ListStyle.Ordered:
                    item = GetOrderedItem(currentLine);
                    break;
                case ListStyle.Unordered:
                    item = GetUnorderedItem(currentLine);
                    break;
            }

            return string.IsNullOrEmpty(item) ? null : item.Trim();
        }

        private string GetUnorderedItem(string currentLine)
        {
            if (currentLine.StartsWith("* "))
                return CleanUnorderedLine(currentLine);

            if (currentLine.StartsWith("- "))
                return CleanUnorderedLine(currentLine);

            return null;
        }

        private string GetOrderedItem(string currentLine)
        {
            return CleanOrderedLine(currentLine);
        }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (GetItem(currentLine) != null)
                return false;

            if (!string.IsNullOrEmpty(currentLine) && currentLine.StartsWith("   "))
                return false;

            if (GetItem(nextLine) != null)
                return false;

            if (string.IsNullOrWhiteSpace(currentLine))
                if (!string.IsNullOrEmpty(nextLine) && nextLine.StartsWith("   "))
                    return false;

            if (string.IsNullOrWhiteSpace(currentLine))
                if (!string.IsNullOrEmpty(nextLine) && GetItem(nextLine) != null)
                    return false;

            return true;
        }

        public override bool End()
        {
            // finish hanging item
            var hangingItem = _currentItemBuilder.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(hangingItem))
                _items.Add(hangingItem);

            // do not skip next line
            return false;
        }

        public override void AddLine(string currentLine)
        {
            // new item starts with a list item marker
            string item = GetItem(currentLine);

            // is an item
            if (!string.IsNullOrWhiteSpace(item))
            {
                // finish last item if exists
                if (_currentItemBuilder != null)
                {
                    _items.Add(_currentItemBuilder.ToString());
                }

                _currentItemBuilder = new StringBuilder();
                _currentItemBuilder.Append(item);
            }
            else
            {
                var line = string.Concat(" ", currentLine.Trim());
                _currentItemBuilder.Append(line);
            }
        }

        public override Block Create()
        {
            return new ListBlock(_items) {Style = _style};
        }

        private string Clean(string line)
        {
            switch (Style)
            {
                case ListStyle.Ordered:
                    return CleanOrderedLine(line);
                case ListStyle.Unordered:
                    return CleanUnorderedLine(line);
            }

            return line.Trim();
        }

        private string CleanOrderedLine(string line)
        {
            int possiblyANumberEnd = line.IndexOf('.');
            if (possiblyANumberEnd > -1)
            {
                string possiblyANumber = line.Substring(0, possiblyANumberEnd);
                int _ = 0;

                if (int.TryParse(possiblyANumber, out _))
                    return line.Substring(possiblyANumberEnd + 1).Trim();
            }

            // not an ordered list item
            return null;
        }

        private string CleanUnorderedLine(string line)
        {
            if (line.StartsWith("* "))
                return line.Substring(1).Trim();

            if (line.StartsWith("- "))
                return line.Substring(1).Trim();

            return line;
        }
    }
}