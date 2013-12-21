namespace Tanka.Markdown.Html
{
    using Blocks;
    using HtmlTags;

    public class OrderedListRenderer : BlockRendererBase<ListBlock>
    {
        protected override bool CanRender(ListBlock block)
        {
            return block.Style == ListStyle.Ordered;
        }

        protected override HtmlTag Render(Document document, ListBlock block)
        {
            var ol = new HtmlTag("ol");

            foreach (var item in block.Items)
            {
                var li = new HtmlTag("li", ol);
                li.Text(item);
            }

            return ol;
        }
    }
}