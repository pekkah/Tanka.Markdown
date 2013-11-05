namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MarkdownParser
    {
        public List<BlockFactoryBase> BlockFactories { get; private set; }

        public MarkdownParser()
        {
            BlockFactories  = new List<BlockFactoryBase>
            {
                new SetextHeadingOneFactory(),
                new SetextHeadingTwoFactory(),
                new HeadingFactory(),
                new ParagraphFactory()
            };
        }

        public MarkdownDocument Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                throw new ArgumentNullException("markdown");

            var blocks = new List<Block>();

            var reader = new LineReader(markdown);
            string currentLine = reader.ReadLine();
            string nextLine = reader.PeekLine();

            Block currentBlock = StartBlock(currentLine, nextLine);

            do
            {
                if (currentBlock.IsEndLine(currentLine, nextLine))
                {
                    // end current block
                    var skipNextLine = currentBlock.End(currentLine);
                    blocks.Add(currentBlock);

                    // some blocks have special end markers which should be skipped
                    if (skipNextLine)
                        reader.ReadLine();

                    if (reader.EndOfDocument)
                        break;

                    // start new block
                    currentLine = reader.ReadLine();
                    nextLine = reader.PeekLine();
                    currentBlock = StartBlock(currentLine, nextLine);
                }
                else
                {
                    currentBlock.AddLine(currentLine);
                }
            } while (reader.EndOfDocument);


            return new MarkdownDocument(blocks);
        }

        private Block StartBlock(string startLine, string nextLine)
        {
            return BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}