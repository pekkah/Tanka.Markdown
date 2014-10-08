namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Inline;
    using System;
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class BlockQuoteBlockBuilder : IBlockBuilder
    {
        private readonly Regex _expression;

        public BlockQuoteBlockBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .StartOfLine()
                .Anything()
                .Find(">")
                .Anything()
                .EndOfLine()
                .ToRegex();
        }
        public bool CanBuild(int start, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, start);
            return isMatch;
        }

        private readonly IBlockBuilder headingBlockBuilder = new HeadingBuilder();

        public Codeblock BuildCodeBlock()
        {
            return new Codeblock(null,0,0);
        }

        public Block Build(int start, StringRange content, out int end)
        {
            end = content.EndOfLine(start);
            var match =_expression.Match(content.Document, start);
            var childBlocks = new List<Block>();
            if (match.Success)
            {
                if (start == end)
                {
                    //empty block quote
                    return new BlockQuote(content, 1, 0);
                }
           
                var quotelinesWithoutChar =
                    LinesFor(content.Document.Substring(start))
                        .Where(line => _expression.IsMatch(line))
                        .Select(line => line.TrimStart(' ').Substring(1).TrimStart(' '));

                foreach (var blockQuoteLine in quotelinesWithoutChar)
                {
                    var s = content.IndexOf(blockQuoteLine);
                    var e = s + blockQuoteLine.Length + 2;
                    end = e;
                    var strRange = new StringRange(blockQuoteLine, 0, blockQuoteLine.Length-1);
                    // only go up to new line
                    if (headingBlockBuilder.CanBuild(0, new StringRange(strRange + Environment.NewLine)))
                    {
                        var hend = 1;
                        var heading = headingBlockBuilder.Build(s, content, out hend);
                        childBlocks.Add(heading);
                        continue;
                    }
                    if (e > s)
                    {
                        var p = childBlocks.FirstOrDefault(pa => pa is Paragraph);
                        var pContent = new StringRange(blockQuoteLine);
                        if (p == null)
                        {
                            p = new Paragraph(pContent, pContent.Start, pContent.End, new[] { new TextSpan(pContent, pContent.Start, pContent.End) });
                            childBlocks.Add(p);
                        }
                        else
                        {
                            childBlocks.Remove(p);
                            pContent =
                                new StringRange(new StringBuilder().AppendLine(p.Document).Append(blockQuoteLine).ToString());
                            p = new Paragraph(pContent, pContent.Start, pContent.End, new[] { new TextSpan(pContent, pContent.Start, pContent.End,false) });
                            childBlocks.Add(p);
                        }
                    }
                }
            }
            return new BlockQuote(content, start + 1, end-1, childBlocks.ToArray());
        }

        private IEnumerable<string> LinesFor(string source)
        {
            var lines = source.Split(new [] {Environment.NewLine}, StringSplitOptions.None);
            return lines;
        }
    }
}
