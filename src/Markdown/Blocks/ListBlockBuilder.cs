namespace Tanka.Markdown.Blocks
{
    using System;
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
        private StringBuilder _currentItemBuilder;
        private readonly ListStyle _style;

        public ListBlockBuilder(ListStyle style)
        {
            _style = style;
            _items = new List<string>();
        }

        public ListStyle Style
        {
            get
            {
                return _style;
            }
        }

        private string GetItem(string currentLine)
        {
            switch (Style)
            {
                case ListStyle.Ordered:
                    return GetOrderedItem(currentLine);
                case ListStyle.Unordered:
                    return GetUnorderedItem(currentLine);
            }

            return null;
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
            // current line is empty and next line is not an item so must be end
            if (string.IsNullOrEmpty(currentLine))
                if (GetItem(nextLine) == null)
                    return true;

            if (string.IsNullOrEmpty(currentLine))
                if (string.IsNullOrEmpty(nextLine))
                    return true;

            return false;
        }

        public override bool End()
        {
            // finish hanging item
            _items.Add(_currentItemBuilder.ToString());

            // do not skip next line
            return false;
        }

        public override void AddLine(string currentLine)
        {
            // new item starts with a list item marker
            var item = GetItem(currentLine);

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
                _currentItemBuilder.Append(currentLine.Trim());
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

            return line;
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