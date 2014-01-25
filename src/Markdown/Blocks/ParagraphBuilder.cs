namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class ParagraphBuilder : IBlockBuilder
    {
        public bool CanBuild(int start, StringRange content)
        {
            return true;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            end = start;
            return null;
        }
    }
}