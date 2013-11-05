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
                new SetextHeadingTwoFactory(),
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

            Block currentBlock = null;

            while (reader.EndOfDocument == false)
            {
                string currentLine = reader.ReadLine();
                string nextLine = reader.PeekLine();

                // start new block if current null
                if (currentBlock == null)
                    currentBlock = StartBlock(currentLine, nextLine);

                if (currentBlock.IsEndLine(currentLine, nextLine))
                {
                    // end current block
                    bool skipNextLine = currentBlock.End(currentLine);
                    blocks.Add(currentBlock);

                    // reset current block
                    currentBlock = null;

                    // some blocks have special end markers which should be skipped
                    if (skipNextLine)
                        reader.ReadLine();
                }
                else
                {
                    currentBlock.AddLine(currentLine);
                }
            }

            return new MarkdownDocument(blocks);
        }

        private Block StartBlock(string startLine, string nextLine)
        {
            return BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}