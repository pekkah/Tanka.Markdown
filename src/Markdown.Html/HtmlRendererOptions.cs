namespace Tanka.Markdown.Html
{
    using System.Collections.Generic;
    using System.Linq;

    public class HtmlRendererOptions
    {
        public static HtmlRendererOptions Defaults
        {
            get
            {
                return new HtmlRendererOptions
                {
                    Renderers = new List<IBlockRenderer>
                    {
                        new BlockQuoteRenderer(),
                        new HeadingRenderer(),
                        new ParagraphRenderer(),
                        new CodeblockRenderer(),
                        new UnorederedListRenderer(),
                        new OrderedListRenderer(),
                        new NullRenderer()
                        
                    }
                };
            }
        }

        public List<IBlockRenderer> Renderers { get; set; }

        public List<ISpanRenderer> SpanRendererss
        {
            get { return Renderers.OfType<ParagraphRenderer>().Single().SpanRenderers; }
        }
    }
}