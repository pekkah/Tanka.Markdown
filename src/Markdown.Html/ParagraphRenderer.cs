namespace Tanka.Markdown.Html
{
    using System.Linq;
    using System.Text;
    using Blocks;
    using HtmlTags;
    using Text;

    public class ParagraphRenderer : BlockRendererBase<Paragraph>
    {
        protected override HtmlTag Render(Document document, Paragraph block)
        {
            var builder = new StringBuilder();
            builder.Append("<p>");

            foreach (ISpan span in block.Content.ToList())
            {
                WriteSpan(builder, span);
            }

            builder.Append("</p>");
            var tag = new LiteralTag(builder.ToString());
            return tag;
        }

        private void WriteSpan(StringBuilder builder, ISpan span)
        {
            if (span is TextSpan)
            {
                var text = span as TextSpan;
                builder.Append(text.Content);
            }
            else if (span is LinkSpan)
            {
                var link = span as LinkSpan;
                var linkTag = new LinkTag(link.Title, link.UrlOrKey);
                builder.Append(linkTag.ToHtmlString());
            }
            else if (span is ImageSpan)
            {
                var image = span as ImageSpan;
                var imageTag = new HtmlTag("img");
                imageTag.Attr("src", image.UrlOrKey);
                imageTag.Attr("alt", image.AltText);
                builder.Append(imageTag.ToHtmlString());
            }
        }
    }
}