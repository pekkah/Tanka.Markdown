namespace Tanka.Markdown.Html
{
    using System;
    using System.Web.UI.HtmlControls;
    using Blocks;
    using HtmlTags;

    public class HeadingRenderer : BlockRendererBase<Heading>
    {
        protected override HtmlTag Render(Document document, Heading block)
        {
            var tag = new HtmlTag(String.Format("h{0}", block.Level));
            tag.Text(block.Text);

            return tag;
        }
    }

    public class OrderedListRenderer : BlockRendererBase<ListBlock>
    {
        protected override bool CanRender(ListBlock block)
        {
            return block.Style == ListStyle.Ordered;
        }

        protected override HtmlTag Render(Document document, ListBlock block)
        {
            var ol = new HtmlTag("ol");

            return ol;
        }
    }
}