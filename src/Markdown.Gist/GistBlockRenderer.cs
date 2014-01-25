namespace Tanka.Markdown.Gist
{
    using Html;
    using HtmlTags;

    public class GistBlockRenderer : BlockRendererBase<GistBlock>
    {
        protected override HtmlTag Render(Document document, GistBlock block)
        {
            // sample: <script src="https://gist.github.com/pekkah/8304465.js"></script>
            var tag = new HtmlTag("script");
            tag.UnencodedAttr("src", CreateGistUrl(block.UserName.ToString(), block.GistId.ToString()));

            return tag;
        }

        private string CreateGistUrl(string userName, string gistId)
        {
            return string.Format("https://gist.github.com/{0}/{1}.js", userName, gistId);
        }
    }
}