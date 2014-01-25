namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public interface IBlockBuilder
    {
        bool CanBuild(int start, StringRange content);

        Block Build(int start, StringRange content, out int end);
    }
}