namespace Tanka.Markdown.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using HtmlTags;

    public class HtmlRenderer
    {
        private readonly List<IBlockRenderer> _renderers;

        public HtmlRenderer()
        {
            _renderers = new List<IBlockRenderer>()
            {
                new HeadingRenderer(),
                new NullRenderer()
            };
        }

        public string Render(Document document)
        {
            if (document == null) throw new ArgumentNullException("document");

            IEnumerable<Block> blocks = document.Blocks;

            var builder = new StringBuilder();

            foreach (Block block in blocks)
            {
                IBlockRenderer renderer = GetBlockRenderer(block);
                HtmlTag rootTag = renderer.Render(block);
                var html = rootTag.ToHtmlString();

                builder.Append(html);
            }

            return builder.ToString();
        }

        protected IBlockRenderer GetBlockRenderer(Block block)
        {
            return _renderers.First(r => r.CanRender(block));
        }
    }
}