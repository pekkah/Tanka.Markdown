namespace Tanka.Markdown.Text
{
    public class LinkSpan : ISpan
    {
        public string Title { get; set; }

        public string UrlOrKey { get; set; }

        public bool IsKey { get; set; }
    }
}