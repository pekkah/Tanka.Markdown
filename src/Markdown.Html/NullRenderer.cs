namespace Tanka.Markdown.Html
{
    using HtmlTags;

    public class NullRenderer : IBlockRenderer
    {
        public bool CanRender(Block block)
        {
            return true;
        }

        public HtmlTag Render(Document document, Block block)
        {
            return HtmlTag.Empty();
        }
    }
}