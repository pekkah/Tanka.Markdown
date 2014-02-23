namespace Tanka.MarkdownTests.Blocks
{
    using System.Text;

    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using Xunit;

    public class CodeblockBuilderFacts
    {
        [Fact]
        public void Content()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("```");
            builder.AppendLine("public int X = 1;");
            builder.AppendLine("```");
            var markdown = new StringRange(builder.ToString());

            var codeblockBuilder = new CodeblockBuilder();
            int end = -1;

            /* when */
            var result = codeblockBuilder.Build(0, markdown, out end);

            /* then */
            result.Start.ShouldBeEquivalentTo(5); // beginning of first line of content
            result.End.ShouldBeEquivalentTo(23); // end of the line before the last ```
            result.ToString().ShouldBeEquivalentTo("public int X = 1;\r\n");

            // end of the block
            end.ShouldBeEquivalentTo(26);
        }
    }
}