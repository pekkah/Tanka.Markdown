namespace Tanka.Markdown.Html
{
    using Blocks;
    using HtmlTags;

    public class UnorederedListRenderer : BlockRendererBase<ListBlock>
    {
        protected override bool CanRender(ListBlock block)
        {
            return block.Style == ListStyle.Unordered;
        }

        protected override HtmlTag Render(Document document, ListBlock block)
        {
            var ol = new HtmlTag("ul");

            foreach (var item in block.Items)
            {
                var li = new HtmlTag("li", ol);
                li.Text(item);
            }

            return ol;
        }
    }
}