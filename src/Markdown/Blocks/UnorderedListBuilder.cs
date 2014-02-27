namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Inline;

    public class UnorderedListBuilder : ListBuilder
    {
        private readonly InlineParser _inlineParser;
        private readonly char _startsWith;

        public UnorderedListBuilder(char startsWith)
        {
            _startsWith = startsWith;
            _inlineParser = new InlineParser();
        }

        public override Block Build(int start, StringRange content, out int end)
        {
            int startOfLine = start;
            var items = new List<Item>();
            bool foundItem = false;
            Item lastItem = null;

            do
            {
                int startOfItem = content.IndexOf(' ', startOfLine) + 1;
                int endOfItem = FindEndOfItem(content, startOfItem);

                IEnumerable<Span> spans = _inlineParser.Parse(new StringRange(content, startOfItem, endOfItem));
                lastItem = new Item(
                    content,
                    startOfItem,
                    endOfItem,
                    spans);

                items.Add(lastItem);
                startOfLine = content.StartOfNextLine(endOfItem);

                if (startOfLine == -1)
                    break;

                foundItem = content.HasCharactersAt(startOfLine, _startsWith, ' ');
            } while (foundItem);

            // special case when content ends
            end = content.EndOfLine(lastItem.End);

            return new List(content, start, end, false, items);
        }

        public override bool CanBuild(int start, StringRange content)
        {
            if (!content.IsStartOfLine(start))
                return false;

            bool starts = content.HasCharactersAt(start, _startsWith, ' ');

            return starts;
        }
    }
}