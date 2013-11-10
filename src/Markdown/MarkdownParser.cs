namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;

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

                if (currentBlockBuilder.IsEndLine(currentLine, nextLine))
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

            return new Document(blocks);
        }

        private BlockBuilderBase CreateBuilder(string startLine, string nextLine)
        {
            return BlockFactories.First(f => f.IsMatch(startLine, nextLine)).Create();
        }
    }
}