namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Inline;

    public class OrderedListBuilder : ListBuilder
    {
        private readonly Regex _expression;
        private readonly InlineParser _inlineParser;

        public OrderedListBuilder()
        {
            _expression = new Regex(@"\G([0-9]{1,3})(\.)(\s)(.*)(\n|\r\n)");
            _inlineParser = new InlineParser();
        }

        public override Block Build(int start, StringRange content, out int end)
        {
            int startOfLine = start;
            var items = new List<Item>();
            Item lastItem = null;
            bool foundItem = false;

            do
            {
                int startOfItem = content.IndexOf(' ', startOfLine) + 1;
                int endOfItem = FindEndOfItem(content, startOfItem);

                IEnumerable<Span> spans = _inlineParser.Parse(new StringRange(content, startOfItem, endOfItem));
                lastItem= new Item(
                    content,
                    startOfItem,
                    endOfItem,
                    spans);

                items.Add(lastItem);

                startOfLine = content.StartOfNextLine(endOfItem);

                if (startOfLine == -1)
                    break;

                foundItem = _expression.IsMatch(content.Document, startOfLine);
            } while (foundItem);

            // special case when content ends
            end = content.EndOfLine(lastItem.End);

            return new List(content, start, end, true, items);
        }

        public override bool CanBuild(int start, StringRange content)
        {
            if (!content.IsStartOfLine(start))
                return false;

            bool isMatch = _expression.IsMatch(content.Document, start);
            return isMatch;
        }
    }
}