namespace Tanka.Markdown
{
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;
    using Inline;

    public class MarkdownParser
    {
        private readonly bool _skipEmptyLines;
        private readonly List<IBlockBuilder> _builders;

        public MarkdownParser(bool skipEmptyLines = true)
        {
            _skipEmptyLines = skipEmptyLines;

            _builders = new List<IBlockBuilder>();
            _builders.Add(new CodeblockBuilder());
            _builders.Add(new EmptyLineBuilder());
            _builders.Add(new HeadingBuilder());
            _builders.Add(new SetextLevelOneHeadingBuilder());
            _builders.Add(new SetextLevelTwoHeadingBuilder());
            _builders.Add(new UnorderedListBuilder('*'));
            _builders.Add(new UnorderedListBuilder('-'));
            _builders.Add(new OrderedListBuilder());
            _builders.Add(new LinkDefinitionListBuilder());

            // the special paragraph builder should always
            // be the last one as it's very greedy!
            _builders.Add(new ParagraphBuilder());
        }

        public Document Parse(string markdown)
        {
            var blocks = new List<Block>();
            var document = new StringRange(markdown);

            foreach (var block in ParseBlocks(document))
            {
                if (_skipEmptyLines && block is EmptyLine)
                    continue;

                blocks.Add(block);
            }

            ResolveReferences(blocks);

            return new Document(blocks, markdown);
        }

        private void ResolveReferences(List<Block> blocks)
        {
            var linkDefinitions = blocks.OfType<LinkDefinitionList>()
                .SelectMany(list => list.Definitions).ToList();

            foreach (var paragraph in blocks.OfType<Paragraph>())
            {
                foreach (var referenceLink in paragraph.Spans.OfType<ReferenceLinkSpan>().ToList())
                {
                    var key = referenceLink.Key.ToString();

                    var definition = linkDefinitions.FirstOrDefault(def => def.Key.ToString() == key);

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

        private IEnumerable<Block> ParseBlocks(StringRange document)
        {
            int paragraphStart = -1;
            int paragraphEnd = -1;
            bool paragraphStarted = false;

            for (int start = 0; start < document.Length; start++)
            {
                var builder = GetBuilder(start, document);

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
                    yield return new Paragraph(document, paragraphStart, paragraphEnd);
                    paragraphStarted = false;
                }

                yield return builder.Build(start, document, out start);
            }

            // so one monster got away
            // run after it as it's yielding
            if (paragraphStarted)
            {
                yield return new Paragraph(document, paragraphStart, paragraphEnd);
            }
        }

        private IBlockBuilder GetBuilder(int start, StringRange document)
        {
            return _builders.First(builder => builder.CanBuild(start, document));
        }
    }
}