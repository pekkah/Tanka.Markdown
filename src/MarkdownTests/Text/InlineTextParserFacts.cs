namespace Tanka.MarkdownTests.Text
{
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class InlineTextParserFacts
    {
        [Fact]
        public void ParseText()
        {
            const string text = "Hello world!";

            var parser = new InlineTextParser();
            var result = parser.Parse(text);

            result.Should().ContainSingle(span => span.As<TextSpan>().Content == text);
        }
    }
}