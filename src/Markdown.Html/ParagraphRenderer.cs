namespace Tanka.Markdown.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Blocks;
    using HtmlTags;
    using Inline;

    public class TextSpanRenderer : ISpanRenderer
    {
        public Func<string, string> CleanInput = input =>
        {
            return input.Replace("\r", "").Replace("\n", " ");
        };

        public bool CanRender(Span span)
        {
            return span is TextSpan;
        }

        public void Render(Span span, StringBuilder builder)
        {
            var text = CleanInput(span.ToString());
            builder.Append(text);
        }
    }

    public class ParagraphRenderer : BlockRendererBase<Paragraph>
    {
        public ParagraphRenderer()
        {
            SpanRenderers = new List<ISpanRenderer>
            {
                new TextSpanRenderer(),
                new SpanRenderer<LinkSpan>((link, builder) =>
                {
                    var linkTag = new LinkTag(link.Title.ToString(), link.Url.ToString());
                    builder.Append(linkTag.ToHtmlString());
                }),
                new SpanRenderer<ReferenceLinkSpan>((link, builder) =>
                {
                    var linkTag = new LinkTag(link.Title.ToString(), link.Key.ToString());
                    builder.Append(linkTag.ToHtmlString());
                }),
                new SpanRenderer<ImageSpan>((image, builder) =>
                {
                    var imageTag = new HtmlTag("img");
                    imageTag.Attr("src", image.Url.ToString());
                    imageTag.Attr("alt", image.Title.ToString());
                    builder.Append(imageTag.ToHtmlString());
                }),
                new SpanRenderer<CodeblockSpan>((codeblockSpan, builder) =>
                {
                    var code = new HtmlTag("code");
                    code.Text(codeblockSpan.ToString());
                    builder.Append(code.ToHtmlString());
                }),
                new SpanRenderer<NewLineSpan>((newLine, builder) => builder.Append(" ")),
                new OpenAndCloseRenderer<StrongEmphasis>("strong"),
                new OpenAndCloseRenderer<Emphasis>("em")
            };
        }

        public List<ISpanRenderer> SpanRenderers { get; set; }

        protected override HtmlTag Render(Document document, Paragraph block)
        {
            var builder = new StringBuilder();
            builder.Append("<p>");

            foreach (Span span in block.Spans)
            {
                ISpanRenderer renderer = SpanRenderers.FirstOrDefault(r => r.CanRender(span));

                if (renderer == null)
                    throw new InvalidOperationException(
                        string.Format("Cannot find renderer for span {0}", span));

                renderer.Render(span, builder);
            }

            builder.Append("</p>");
            var tag = new LiteralTag(builder.ToString());
            return tag;
        }
    }
}