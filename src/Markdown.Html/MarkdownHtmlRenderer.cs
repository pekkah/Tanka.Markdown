namespace Tanka.Markdown.Html
{
    using System;
    using System.Linq;
    using System.Text;
    using Blocks;
    using HtmlTags;

    public class MarkdownHtmlRenderer
    {
        public MarkdownHtmlRenderer()
        {
            Options = HtmlRendererOptions.Defaults;
        }

        public MarkdownHtmlRenderer(HtmlRendererOptions options)
        {
            Options = options;
        }

        public HtmlRendererOptions Options { get; private set; }

        public string Render(Document document)
        {
            if (document == null) throw new ArgumentNullException("document");

            var builder = new StringBuilder();

            if (!document.Blocks.Any())
                return string.Empty;

            foreach (Block block in document.Blocks)
            {
                IBlockRenderer renderer = null;

                try
                {
                    renderer = GetBlockRenderer(block);
                    HtmlTag rootTag = renderer.Render(document, block);
                    string html = rootTag.ToHtmlString();

                    builder.Append(html);
                }
                catch (Exception x)
                {
                    throw new RenderingException(
                        "Rendering exception. See exception details for more info.",
                        x)
                    {
                        Block = block,
                        Renderer = renderer
                    };
                }
            }

            return builder.ToString();
        }

        protected IBlockRenderer GetBlockRenderer(Block block)
        {
            return Options.Renderers.First(r => r.CanRender(block));
        }
    }

    public class RenderingException : Exception
    {
        public RenderingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public Block Block { get; set; }

        public IBlockRenderer Renderer { get; set; }
    }
}