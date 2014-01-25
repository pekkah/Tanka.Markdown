namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Markdown;

    public class LinkDefinitionListBuilder : IBlockBuilder
    {
        private readonly Regex _expression;

        public LinkDefinitionListBuilder()
        {
            _expression = new Regex(@"\G(\[.*\]):(.*)");
        }

        public bool CanBuild(int start, StringRange content)
        {
            // [key]: url
            // [123]: url
            var isMatch = _expression.IsMatch(content.Document, start);

            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            int startOfLine = start;
            var items = new List<LinkDefinition>();

            do
            {
                var key = new StringRange(
                    content,
                    content.IndexOf('[', startOfLine) + 1,
                    content.IndexOf(']', startOfLine) - 1);

                var urlStart = content.IndexOf(':', key.End) + 1;

                if (content[urlStart] == ' ')
                    urlStart++;

                var url = new StringRange(
                    content,
                    urlStart,
                    content.EndOfLine(startOfLine));

                var item = new LinkDefinition(
                    content,
                    startOfLine,
                    content.EndOfLine(startOfLine),
                    key,
                    url);

                items.Add(item);
                startOfLine = content.StartOfNextLine(startOfLine);

                if (startOfLine == -1)
                    break;

            } while (_expression.IsMatch(content.Document, content.IndexOf('[', startOfLine)));

            // special case when content ends
            end = startOfLine != -1 ? content.EndOfLine(startOfLine, true) : content.End;

            return new LinkDefinitionList(content, start, end, items);
        }
    }
}