namespace Tanka.MarkdownTests.Inline
{
    using System.Linq;
    using FluentAssertions;
    using Markdown;
    using Markdown.Inline;
    using Xunit;

    public class InlineCodeblock
    {
        [Fact]
        public void Codeblock()
        {
            /* given */
            var text = new StringRange("`bool IsCode = true;`");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().OnlyContain(span => span is CodeblockSpan);

            // verify link details
            var codeSpan = result.First() as CodeblockSpan;
            codeSpan.ToString().ShouldBeEquivalentTo("bool IsCode = true;");
        }
    }
}