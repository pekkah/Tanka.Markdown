namespace Tanka.Markdown.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Blocks;
    using HtmlTags;

    public class HtmlRenderer
    {
        public HtmlRenderer()
        {
            Options = HtmlRendererOptions.Defaults;
        }

        public HtmlRenderer(HtmlRendererOptions options)
        {
            Options = options;
        }

        public HtmlRendererOptions Options { get; private set; }

        public string Render(Document document)
        {
            if (document == null) throw new ArgumentNullException("document");

            IEnumerable<Block> blocks = document.Blocks;

            var builder = new StringBuilder();

            foreach (Block block in blocks)
            {
                IBlockRenderer renderer = GetBlockRenderer(block);
                HtmlTag rootTag = renderer.Render(document, block);
                string html = rootTag.ToHtmlString();

                builder.Append(html);
            }

            return builder.ToString();
        }

        protected IBlockRenderer GetBlockRenderer(Block block)
        {
            return Options.Renderers.First(r => r.CanRender(block));
        }
    }
}