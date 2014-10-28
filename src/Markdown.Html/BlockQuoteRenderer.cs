namespace Tanka.Markdown.Html
{
    using HtmlTags;
    using Blocks;

    /// <summary>
    /// Should render <blockquote>My Quote</blockquote> for
    /// <see cref="BlockQuote"/> blocks. 
    /// <example>
    /// <value>
    ///     > My Quote
    /// </value>
    /// will become:
    /// <value>
    ///     <blockquote>My Quote</blockquote>
    /// </value>
    /// </example>
    /// </summary>
    public class BlockQuoteRenderer :  BlockRendererBase<BlockQuote>
    {
        protected ParagraphRenderer ParagraphRenderer = new ParagraphRenderer();
        protected HeadingRenderer HeadingRenderer = new HeadingRenderer();

        protected override HtmlTag Render(Document document, BlockQuote block)
        {
            var tag = new HtmlTag("blockquote");
            foreach (var childBlock in block.ChildBlocks)
            {
                if (HeadingRenderer.CanRender(childBlock))
                {
                    var heading = HeadingRenderer.Render(document, childBlock);
                    tag.Append(heading);
                    continue;
                }
                if (ParagraphRenderer.CanRender(childBlock))
                {
                    var pTag = ParagraphRenderer.Render(document, childBlock);
                    tag.Append(pTag);
                }
            }
            return tag;
        }
    }
}
