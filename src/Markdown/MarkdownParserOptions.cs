namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;
    using Text;

    public class MarkdownParserOptions
    {
        private static readonly Lazy<MarkdownParserOptions> DefaultsLazy = new Lazy<MarkdownParserOptions>(CreateDefauts);

        public static MarkdownParserOptions Defaults
        {
            get { return DefaultsLazy.Value; }
        }

        public bool AutoResolveReferenceLinks { get; set; }

        public StringTokenizer Tokenizer
        {
            get { return BlockFactories.OfType<ParagraphFactory>().Single().Tokenizer; }

            set { BlockFactories.OfType<ParagraphFactory>().Single().Tokenizer = value; }
        }

        public List<BlockFactoryBase> BlockFactories { get; set; }

        public List<SpanFactoryBase> SpanFactories
        {
            get
            {
                return BlockFactories.OfType<ParagraphFactory>().Single().SpanFactories;
            }
        }

        public bool SkipEmptyLines { get; set; }

        private static MarkdownParserOptions CreateDefauts()
        {
            var options =  new MarkdownParserOptions
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
                    new ParagraphFactory()
                }
            };

            options.SpanFactories.AddRange(new SpanFactoryBase[] {
                new EndFactory(),
                new ImageSpanFactory(),
                new LinkSpanFactory(),
                new ReferenceLinkSpanFactory(),
                new EmphasisFactory(),
                new TextSpanFactory(),
                new UnknownAsTextSpanFactory()
            });

            return options;
        }
    }
}