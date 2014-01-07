namespace Tanka.Markdown.Html
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Blocks;
    using HtmlTags;
    using Text;

    public class ParagraphRenderer : BlockRendererBase<Paragraph>
    {
        public ParagraphRenderer()
        {
            SpanRenderers = new List<ISpanRenderer>
            {
                new SpanRenderer<TextSpan>((text, builder) => builder.Append(text.Content)),
                new SpanRenderer<LinkSpan>((link, builder) =>
                {
                    var linkTag = new LinkTag(link.Title, link.UrlOrKey);
                    builder.Append(linkTag.ToHtmlString());
                }),
                new SpanRenderer<ImageSpan>((image, builder) =>
                {
                    var imageTag = new HtmlTag("img");
                    imageTag.Attr("src", image.UrlOrKey);
                    imageTag.Attr("alt", image.AltText);
                    builder.Append(imageTag.ToHtmlString());
                })
            };
        }

        public List<ISpanRenderer> SpanRenderers { get; set; }

        protected override HtmlTag Render(Document document, Paragraph block)
        {
            var builder = new StringBuilder();
            builder.Append("<p>");

            foreach (ISpan span in block.Content.ToList())
            {
                ISpanRenderer renderer = SpanRenderers.First(r => r.CanRender(span));
                renderer.Render(span, builder);
            }

            builder.Append("</p>");
            var tag = new LiteralTag(builder.ToString());
            return tag;
        }
    }
}