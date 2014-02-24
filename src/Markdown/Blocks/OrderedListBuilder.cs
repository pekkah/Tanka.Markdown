namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Inline;
    using Markdown;

    public class OrderedListBuilder : IBlockBuilder
    {
        private readonly Regex _expression;
        private readonly InlineParser _inlineParser;

        public OrderedListBuilder()
        {
            _expression = new Regex(@"\G([0-9]{1,3})(\.)(\s)(.*)(\n|\r\n)");
            _inlineParser = new InlineParser();
        }

        public bool CanBuild(int start, StringRange content)
        {
            bool isMatch = _expression.IsMatch(content.Document, start);
            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            int startOfLine = start;
            var items = new List<Item>();

            bool foundItem = false;

            do
            {
                var startOfItem = content.IndexOf(' ', startOfLine) + 1;
                var endOfItem = FindEndOfItem(content, startOfItem);

                var spans = _inlineParser.Parse(new StringRange(content, startOfItem, endOfItem));
                var item = new Item(
                    content,
                    startOfItem,
                    endOfItem,
                    spans);

                items.Add(item);

                startOfLine = content.StartOfNextLine(endOfItem);
                
                if (startOfLine == -1)
                    break;

                foundItem = _expression.IsMatch(content.Document, startOfLine);
            } while (foundItem);

            // special case when content ends
            end = startOfLine != -1 ? content.EndOfLine(startOfLine, false) : content.End;

            return new List(content, start, end, true, items);
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