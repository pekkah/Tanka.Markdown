namespace Tanka.MarkdownTests.Text
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class InlineTextParserFacts
    {
        [Fact]
        public void ParseText()
        {
            const string text = "Hello world!";

            var parser = new TextSpanParser();
            IEnumerable<ISpan> result = parser.Parse(text);

            result.Should().ContainSingle(span => span.As<TextSpan>().Content == text);
        }

        [Fact]
        public void ParseLink()
        {
            const string text = "[here](http://test.com)";

            var parser = new TextSpanParser();
            IEnumerable<ISpan> result = parser.Parse(text);

            result.Should()
                .ContainSingle(
                    span => span.As<LinkSpan>().Title == "here" && span.As<LinkSpan>().UrlOrKey == "http://test.com");
        }

        [Fact]
        public void ParseTextAndLinkAndText()
        {
            const string text = "text [here](http://test.com) text";

            var parser = new TextSpanParser();
            List<ISpan> result = parser.Parse(text).ToList();

            result.Should().HaveCount(3);
            var first = result[0] as TextSpan;
            var link = result[1] as LinkSpan;
            var third = result[2] as TextSpan;

            first.Content.ShouldBeEquivalentTo("text ");
            link.Title.ShouldAllBeEquivalentTo("here");
            link.UrlOrKey.ShouldBeEquivalentTo("http://test.com");
            third.Content.ShouldAllBeEquivalentTo(" text");
        }
    }
}