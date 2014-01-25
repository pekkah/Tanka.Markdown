namespace Tanka.Markdown.Gist
{
    using Blocks;

    public class GistBlock : Block
    {
        public GistBlock(
            StringRange parent,
            int start,
            int end,
            StringRange userName,
            StringRange gistId) : base(parent, start, end)
        {
            UserName = userName;
            GistId = gistId;
        }

        public StringRange UserName { get; set; }
        public StringRange GistId { get; set; }
    }
}