namespace Tanka.Markdown.Blocks
{
    using Markdown;

    public class LinkDefinition : Block
    {
        public LinkDefinition(
            StringRange parent,
            int start,
            int end,
            StringRange key,
            StringRange url) : base(parent, start, end)
        {
            Key = key;
            Url = url;
        }

        public StringRange Key { get; private set; }

        public StringRange Url { get; private set; }
    }
}