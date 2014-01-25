namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class EmptyLineBuilder : IBlockBuilder
    {
        public bool CanBuild(int start, StringRange content)
        {
            if (start < 2)
            {
                return content.StartsWith('\r', '\n');
            }

            // range of should fit \r\n\r\n
            var range = new StringRange(content, start - 2, start + 2);

            return range.StartsWith('\r', '\n', '\r', '\n');
        }

        public Block Build(int start, StringRange content, out int end)
        {
            end = content.EndOfLine(start, true);
            return new EmptyLine(content, start, end);
        }
    }
}