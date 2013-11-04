namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.SqlServer.Server;

    public class MarkdownParser
    {
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
                    currentBlock.End(currentLine);
                    blocks.Add(currentBlock);
                    
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
            var headingFactory = new HeadingFactory();

            if (headingFactory.IsMatch(startLine, nextLine))
            {
                return headingFactory.Create();
            }

            return new Paragraph(); ;
        }
    }
}