namespace Tanka.Markdown.Html
{
    using Blocks;
    using HtmlTags;

    public class UnorederedListRenderer : BlockRendererBase<List>
    {
        protected override bool CanRender(List block)
        {
            return !block.IsOrdered;
        }

        protected override HtmlTag Render(Document document, List block)
        {
            var ol = new HtmlTag("ul");

            foreach (var item in block.Items)
            {
                var li = new HtmlTag("li", ol);
                li.Text(item.ToString());
            }

            return ol;
        }
    }
}