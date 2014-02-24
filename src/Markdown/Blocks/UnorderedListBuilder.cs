namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Inline;
    using Markdown;

    public class UnorderedListBuilder : IBlockBuilder
    {
        private readonly char _startsWith;
        private readonly InlineParser _inlineParser;

        public UnorderedListBuilder(char startsWith)
        {
            _startsWith = startsWith;
            _inlineParser = new InlineParser();
        }

        public bool CanBuild(int start, StringRange content)
        {
            bool starts = content.HasCharactersAt(start, _startsWith, ' ');

            if (!starts)
                return false;

            bool nextLineStarts = content.HasCharactersAt(
                content.StartOfNextLine(start),
                _startsWith, ' ');

            return nextLineStarts;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            int startOfLine = start;
            var items = new List<Item>();

            do
            {
                var startOfItem = startOfLine + 2;
                var endOfItem = FindEndOfItem(content, startOfItem);

                var spans = _inlineParser.Parse(new StringRange(content, startOfItem, endOfItem));
                var item = new Item(
                    content,
                    startOfItem,
                    endOfItem,
                    spans);

                items.Add(item);
                startOfLine = content.StartOfNextLine(startOfLine);
            } while (content.HasCharactersAt(startOfLine, _startsWith, ' '));

            // special case when content ends
            end = startOfLine != -1 ? content.EndOfLine(startOfLine, false) : content.End;

            return new List(content, start, end, false, items);
        }

        private int FindEndOfItem(StringRange content, int position)
        {
            /*********************************************
             * item will end when the next line is not 
             * indented by at least one space
             * ******************************************/

            // document end
            if (position > content.End)
                return content.End;

            var startOfNextLine = position;

            do
            {
                startOfNextLine = content.StartOfNextLine(startOfNextLine);

                // document end
                if (startOfNextLine == -1)
                    return content.EndOfLine(content.End);

                if (content[startOfNextLine] != ' ')
                    return content.EndOfLine(startOfNextLine - 1);

            } while (true);
        }
    }
}