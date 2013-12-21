namespace Tanka.Markdown.Html
{
    using System;
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
}