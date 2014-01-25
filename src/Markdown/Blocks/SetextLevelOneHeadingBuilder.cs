namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class SetextLevelOneHeadingBuilder : IBlockBuilder
    {
        public bool CanBuild(int start, StringRange content)
        {
            var startOfNextLine = content.StartOfNextLine(start);
            bool isMatch = content.HasStringAt(startOfNextLine, "===");

            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            int endOfHeadingText = content.EndOfLine(start);

            int startOfMarker = content.StartOfNextLine(endOfHeadingText);
            end = content.StartOfNextLine(startOfMarker) - 1;
            return new Heading(content, start, endOfHeadingText, 1);
        }
    }
}