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
            builder.Append("```\n");
            builder.Append("public int X = 1;\n");
            builder.Append("```\n");
            var markdown = new StringRange(builder.ToString());

            var codeblockBuilder = new CodeblockBuilder();
            int end = -1;

            /* when */
            var result = codeblockBuilder.Build(0, markdown, out end);

            /* then */
            result.ToString().ShouldBeEquivalentTo("public int X = 1;\n");
        }
    }
}