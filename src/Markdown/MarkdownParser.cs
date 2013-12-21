namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;

    public class MarkdownParser
    {
        private readonly bool _skipEmptyLines;

        public MarkdownParser()
        {
            // default settings
            _skipEmptyLines = true;

            BlockFactories = new List<BlockFactoryBase>
            {
                new BlockquoteFactory(),
                new SetextHeadingOneFactory(),
                new HeadingFactory(),
                new CodeblockBuilderFactory(),
                new UnorderedListFactory(),
                new OrderedListFactory(),
                new EmptyLineFactory(),
                new ParagraphFactory()
            };
        }

        public List<BlockFactoryBase> BlockFactories { get; private set; }

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

            if (_skipEmptyLines)
                blocks = blocks.Where(b => b.GetType() != typeof (EmptyLine)).ToList();

            return new Document(blocks);
        }

        private BlockBuilderBase CreateBuilder(string startLine, string nextLine)
        {
            return BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}