namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;
    using Inline;

    public class MarkdownParser
    {
        private readonly List<IBlockBuilder> _builders;
        private readonly bool _skipEmptyLines;

        public MarkdownParser(bool skipEmptyLines = true)
        {
            _skipEmptyLines = skipEmptyLines;

            _builders = new List<IBlockBuilder>
            {
                new EmptyLineBuilder(),
                new CodeblockBuilder(),
                new HeadingBuilder(),
                new SetextLevelOneHeadingBuilder(),
                new SetextLevelTwoHeadingBuilder(),
                new UnorderedListBuilder('*'),
                new UnorderedListBuilder('-'),
                new OrderedListBuilder(),
                new LinkDefinitionListBuilder(),
                new ParagraphBuilder()
            };
        }

        public List<IBlockBuilder> Builders
        {
            get { return _builders; }
        }

        public Document Parse(string markdown)
        {
            var blocks = new List<Block>();

            if (string.IsNullOrWhiteSpace(markdown))
                return new Document(blocks, markdown);

            var markdownContent = new StringRange(markdown);

            foreach (Block block in ParseBlocks(markdownContent))
            {
                // skip empty paragraphs
                var paragraph = block as Paragraph;
                if (paragraph != null)
                {
                    if (paragraph.IsEmpty())
                        continue;
                }

                // skip empty lines
                if (_skipEmptyLines && block is EmptyLine)
                    continue;

                blocks.Add(block);
            }

            ResolveReferences(blocks);

            return new Document(blocks, markdown);
        }

        private void ResolveReferences(List<Block> blocks)
        {
            List<LinkDefinition> linkDefinitions = blocks.OfType<LinkDefinitionList>()
                .SelectMany(list => list.Definitions).ToList();

            foreach (Paragraph paragraph in blocks.OfType<Paragraph>())
            {
                foreach (ReferenceLinkSpan referenceLink in paragraph.Spans.OfType<ReferenceLinkSpan>().ToList())
                {
                    string key = referenceLink.Key.ToString().ToLower();

                    LinkDefinition definition =
                        linkDefinitions.FirstOrDefault(def => def.Key.ToString().ToLower() == key);

                    if (definition == null)
                        continue;

                    // replace the reference with a link
                    var link = new LinkSpan(
                        paragraph,
                        referenceLink.Start,
                        referenceLink.End,
                        referenceLink.Title,
                        definition.Url);

                    paragraph.Replace(referenceLink, link);
                }
            }
        }

        protected IEnumerable<Block> ParseBlocks(StringRange document)
        {
            int paragraphStart = -1;
            int paragraphEnd = -1;
            bool paragraphStarted = false;

            // there should be one monster builder always in builders
            var monsterBuilder = _builders.OfType<ParagraphBuilder>().Single();

            for (int start = 0; start < document.Length; start++)
            {
                IBlockBuilder builder = GetBuilder(start, document);

                // paragraph is special as it just eats
                // everything else the others don't
                // want. Special muncher!
                if (builder is ParagraphBuilder)
                {
                    if (!paragraphStarted)
                    {
                        paragraphStart = start;
                        paragraphStarted = true;
                    }

                    paragraphEnd = start;
                    continue;
                }

                // have to kill the monster so others can
                // feed too
                if (paragraphStarted)
                {
                    // yield it and then kill it!
                    yield return monsterBuilder.Build(paragraphStart, paragraphEnd, document);
                    paragraphStarted = false;
                }

                yield return BuildBlock(builder, start, document, out start);
            }

            // so one monster got away
            // run after it as it's yielding
            if (paragraphStarted)
            {
                yield return monsterBuilder.Build(paragraphStart, paragraphEnd, document);
            }
        }

        private Block BuildBlock(IBlockBuilder builder, int start, StringRange document, out int newPosition)
        {
            try
            {
                return builder.Build(start, document, out newPosition);
            }
            catch (Exception x)
            {
                throw new ParsingException("Parser failed to parse block. See exception details for more info.", x)
                {
                    BuilderType = builder.GetType(),
                    Position = start,
                    Content = new StringRange(document.Document, start, document.End)
                };
            }
        }

        private IBlockBuilder GetBuilder(int start, StringRange document)
        {
            return _builders.First(builder => builder.CanBuild(start, document));
        }
    }
}