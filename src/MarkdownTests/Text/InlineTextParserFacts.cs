namespace Tanka.MarkdownTests.Text
{
    using System.Collections.Generic;
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
            IEnumerable<ISpan> result = parser.Parse(text);

            result.Should().ContainSingle(span => span.As<TextSpan>().Content == text);
        }
    }
}