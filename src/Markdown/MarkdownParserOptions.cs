namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;
    using Text;

    public class MarkdownParserOptions
    {
        public static MarkdownParserOptions Defaults
        {
            get
            {
                var paragraphFactory = new ParagraphFactory();

                return new MarkdownParserOptions
                {
                    // default settings
                    AutoResolveReferenceLinks = true,
                    SkipEmptyLines = true,
                    BlockFactories = new List<BlockFactoryBase>
                    {
                        new BlockquoteFactory(),
                        new SetextHeadingOneFactory(),
                        new HeadingFactory(),
                        new CodeblockBuilderFactory(),
                        new UnorderedListFactory(),
                        new OrderedListFactory(),
                        new EmptyLineFactory(),
                        new LinkDefinitionBuilderFactory(),
                        paragraphFactory
                    }
                };
            }
        }

        public bool AutoResolveReferenceLinks { get; set; }

        public List<SpanFactoryBase> SpanFactories
        {
            get { return BlockFactories.OfType<ParagraphFactory>().Single().SpanFactories; }
        }

        public StringTokenizer Tokenizer
        {
            get { return BlockFactories.OfType<ParagraphFactory>().Single().Tokenizer; }

            set { BlockFactories.OfType<ParagraphFactory>().Single().Tokenizer = value; }
        }

        public List<BlockFactoryBase> BlockFactories { get; set; }

        public bool SkipEmptyLines { get; set; }
    }
}