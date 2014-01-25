namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Markdown;

    public class UnorderedListBuilder : IBlockBuilder
    {
        private readonly char _startsWith;

        public UnorderedListBuilder(char startsWith)
        {
            _startsWith = startsWith;
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
                var item = new Item(
                    content,
                    startOfLine + 2,
                    content.EndOfLine(startOfLine),
                    false);

                items.Add(item);
                startOfLine = content.StartOfNextLine(startOfLine);
            } while (content.HasCharactersAt(startOfLine, _startsWith, ' '));

            // special case when content ends
            end = startOfLine != -1 ? content.EndOfLine(startOfLine, true) : content.End;

            return new List(content, start, end, false, items);
        }
    }
}