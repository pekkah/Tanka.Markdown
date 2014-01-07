namespace Tanka.Markdown.Gist
{
    public class GistBlock : Block
    {
        public GistBlock(string userName, string gistId)
        {
            UserName = userName;
            GistId = gistId;
        }

        public string UserName { get; set; }
        public string GistId { get; set; }
    }
}