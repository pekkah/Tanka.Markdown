namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class Heading : Block
    {
        public Heading(StringRange parent, int start, int end, int level)
            : base(parent, start, end)
        {
            Level = level;
        }

        public int Level { get; private set; }
    }
}