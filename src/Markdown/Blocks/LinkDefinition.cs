namespace Tanka.Markdown.Blocks
{
    public class LinkDefinition : Block
    {
        public LinkDefinition(string key, string url)
        {
            Key = key;
            Url = url;
        }

        public string Key { get; set; }
        public string Url { get; set; }
    }
}