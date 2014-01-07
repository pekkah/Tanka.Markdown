namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;
    using Text;

    public class MarkdownParser
    {
        public MarkdownParser()
        {
            Options = MarkdownParserOptions.Defaults;
        }

        public MarkdownParser(MarkdownParserOptions options)
        {
            Options = options;
        }

        public MarkdownParserOptions Options { get; set; }

        public Document Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                throw new ArgumentNullException("markdown");

            var blocks = new List<Block>();
            var reader = new LineReader(markdown);

            BlockBuilderBase currentBlockBuilder = null;

            while (reader.EndOfDocument == false)
            {
                string currentLine = reader.ReadLine();
                string nextLine = reader.PeekLine();

                // start new block if current null
                if (currentBlockBuilder == null)
                    currentBlockBuilder = CreateBuilder(currentLine, nextLine);

                if (currentBlockBuilder.IsEndLine(currentLine, nextLine) || reader.EndOfDocument)
                {
                    // end current block
                    currentBlockBuilder.AddLine(currentLine);
                    bool skipNextLine = currentBlockBuilder.End();
                    blocks.Add(currentBlockBuilder.Create());

                    // reset current block
                    currentBlockBuilder = null;

                    // some blocks have special end markers which should be skipped
                    if (skipNextLine)
                        reader.ReadLine();
                }
                else
                {
                    currentBlockBuilder.AddLine(currentLine);
                }
            }

            if (Options.SkipEmptyLines)
                blocks = blocks.Where(b => b.GetType() != typeof (EmptyLine)).ToList();

            if (Options.AutoResolveReferenceLinks)
                ResolveLinks(blocks);

            return new Document(blocks);
        }

        private void ResolveLinks(IEnumerable<Block> blocks)
        {
            if (blocks == null) throw new ArgumentNullException("blocks");

            List<Block> allBlocks = blocks.ToList();
            IEnumerable<Paragraph> paragraphs = allBlocks.OfType<Paragraph>();
            List<LinkDefinition> definitions = allBlocks.OfType<LinkDefinition>().ToList();

            foreach (Paragraph paragraph in paragraphs)
            {
                // process all reference links (IsKey == True)
                foreach (LinkSpan link in paragraph.Content.OfType<LinkSpan>().Where(l => l.IsKey))
                {
                    LinkDefinition definition = definitions.FirstOrDefault(d => d.Key == link.UrlOrKey);

                    // todo(pekka): should we fail if the link definition is not found?
                    if (definition == null)
                        continue;

                    link.UrlOrKey = definition.Url;
                }
            }
        }

        private BlockBuilderBase CreateBuilder(string startLine, string nextLine)
        {
            return Options.BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}