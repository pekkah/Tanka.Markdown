namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MarkdownParser
    {
        public MarkdownParser()
        {
            BlockFactories = new List<BlockFactoryBase>
            {
                new BlockquoteFactory(),
                new SetextHeadingOneFactory(),
                new HeadingFactory(),
                new ListBlockFactory(),
                new ParagraphFactory()
            };
        }

        public List<BlockFactoryBase> BlockFactories { get; private set; }

        public MarkdownDocument Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                throw new ArgumentNullException("markdown");

            var blocks = new List<Block>();
            var reader = new LineReader(markdown);

            BlockBuilder currentBlockBuilder = null;

            while (reader.EndOfDocument == false)
            {
                string currentLine = reader.ReadLine();
                string nextLine = reader.PeekLine();

                // start new block if current null
                if (currentBlockBuilder == null)
                    currentBlockBuilder = StartBlock(currentLine, nextLine);

                if (currentBlockBuilder.IsEndLine(currentLine, nextLine))
                {
                    // end current block
                    bool skipNextLine = currentBlockBuilder.End(currentLine);
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

            return new MarkdownDocument(blocks);
        }

        private BlockBuilder StartBlock(string startLine, string nextLine)
        {
            return BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}