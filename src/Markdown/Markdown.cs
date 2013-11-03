namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class MarkdownParser
    {
        public MarkdownDocument Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                throw new ArgumentNullException("markdown");

            var blocks = new List<Block>();
            var reader = new StringReader(markdown);

            string currentLine = reader.ReadLine();
            Block block = StartBlock(currentLine);

            do
            {
                // add line will return true if the line ends the block
                if (block.IsEndLine(currentLine))
                {
                    block.End(currentLine);
                    blocks.Add(block);

                    // start a new block
                    block = StartBlock(currentLine);
                }
                else
                {
                    block.AddLine(currentLine);
                }
            } while ((currentLine = reader.ReadLine()) != null);

            return new MarkdownDocument(blocks);
        }

        private Block StartBlock(string startLine)
        {
            var headingFactory = new HeadingFactory();

            if (headingFactory.IsMatch(startLine))
            {
                return headingFactory.Create();
            }

            return new Paragraph(); ;
        }
    }

    public abstract class Block
    {
        public abstract bool IsEndLine(string currentLine);

        public abstract void End(string currentLine);

        public abstract void AddLine(string currentLine);
    }

    public abstract class BlockFactoryBase
    {
        public abstract bool IsMatch(string currentLine);

        public abstract Block Create();
    }

    public class HeadingFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine)
        {
            if (currentLine.StartsWith("#"))
            {
                return true;
            }

            return false;
        }

        public override Block Create()
        {
            return new Heading();
        }
    }

    public class Paragraph : Block
    {
        private readonly StringBuilder _builder;

        public Paragraph()
        {
            _builder = new StringBuilder();
        }

        public override bool IsEndLine(string currentLine)
        {
            if (string.IsNullOrEmpty(currentLine))
            {
                return true;
            }

            return false;
        }

        public override void End(string currentLine)
        {
            // do not add the empty line to the paragraph
        }

        public override void AddLine(string currentLine)
        {
            _builder.AppendLine(currentLine);
        }
    }

    public class Heading : Block
    {
        public int Level { get; private set; }

        public string Text { get; private set; }

        public override bool IsEndLine(string currentLine)
        {
            return true;
        }

        public override void End(string currentLine)
        {
            int hashesEnd = currentLine.LastIndexOf('#') + 1;
            string hashes = currentLine.Substring(0, hashesEnd);
            Level = hashes.Length <= 6 ? hashes.Length : 6;
            Text = currentLine.Substring(hashesEnd).Trim();
        }

        public override void AddLine(string currentLine)
        {
            throw new NotImplementedException();
        }
    }
}