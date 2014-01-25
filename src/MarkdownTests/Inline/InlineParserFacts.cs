namespace Tanka.MarkdownTests.Inline
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown;
    using Markdown.Inline;
    using Xunit;

    public class InlineParserFacts
    {
        [Fact]
        public void Character()
        {
            /* given */
            var text = new StringRange("s");
            var parser = new InlineParser();

            /* when */
            List<Span> result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().ContainItemsAssignableTo<TextSpan>();

            // verify span details
            var span = result.Single() as TextSpan;
            span.Start.ShouldBeEquivalentTo(0);
            span.End.ShouldBeEquivalentTo(0);
            span.Length.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public void Text()
        {
            /* given */
            var text = new StringRange("random");
            var parser = new InlineParser();

            /* when */
            List<Span> result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().ContainItemsAssignableTo<TextSpan>();

            // verify span details
            var span = result.Single() as TextSpan;
            span.Length.ShouldBeEquivalentTo(text.Length);
        }

        [Fact]
        public void Link()
        {
            /* given */
            var text = new StringRange("[title](http://something.com)");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().OnlyContain(span => span is LinkSpan);

            // verify link details
            var linkSpan = result.First() as LinkSpan;
            linkSpan.Title.ShouldBeEquivalentTo(new StringRange(text, 1, 5));
            linkSpan.Url.ShouldBeEquivalentTo(new StringRange(text, 8, 27));
        }

        [Fact]
        public void TextAndLink()
        {
            /* given */
            var text = new StringRange("0123 [title](http://something.com)");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(2);
            result.First().Should().BeOfType<TextSpan>();
            result.Last().Should().BeOfType<LinkSpan>();

            // verify link details
            var linkSpan = result.Last() as LinkSpan;
            linkSpan.Title.ShouldBeEquivalentTo(new StringRange(text, 6, 10));
            linkSpan.Url.ShouldBeEquivalentTo(new StringRange(text, 13, 32));
        }

        [Fact]
        public void Image()
        {
            /* given */
            var text = new StringRange("![title](http://something.com)");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().OnlyContain(span => span is ImageSpan);

            // verify link details
            var image = result.First() as ImageSpan;
            image.Title.ShouldBeEquivalentTo(new StringRange(text, 2, 6));
            image.Url.ShouldBeEquivalentTo(new StringRange(text, 9, 28));
        }

        [Fact]
        public void ReferenceLink()
        {
            /* given */
            var text = new StringRange("[title][0]");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(1);
            result.Should().OnlyContain(span => span is ReferenceLinkSpan);

            // verify link details
            var linkSpan = result.First() as ReferenceLinkSpan;
            linkSpan.Title.ShouldBeEquivalentTo(new StringRange(text, 1, 5));
            linkSpan.Key.ShouldBeEquivalentTo(new StringRange(text, 8, 8));
        }

        [Fact]
        public void Emphasis()
        {
            /* given */
            var text = new StringRange("*something important here*");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(3);

            result.First().Should().BeOfType<Emphasis>();
            result.First().Start.ShouldBeEquivalentTo(0);

            result.ElementAt(1).Start.ShouldBeEquivalentTo(1);
            result.ElementAt(1).End.ShouldBeEquivalentTo(24);
            result.ElementAt(1).ToString().ShouldAllBeEquivalentTo("something important here");

            result.Last().Start.ShouldBeEquivalentTo(25);
            result.Last().Should().BeOfType<Emphasis>();
        }

        [Fact]
        public void StrongEmphasis()
        {
            /* given */
            var text = new StringRange("**something important here**");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(3);

            result.First().Should().BeOfType<StrongEmphasis>();
            result.First().Start.ShouldBeEquivalentTo(0);

            result.ElementAt(1).Start.ShouldBeEquivalentTo(2);
            result.ElementAt(1).End.ShouldBeEquivalentTo(25);

            result.Last().Start.ShouldBeEquivalentTo(26);
            result.Last().End.ShouldBeEquivalentTo(27);
            result.Last().Should().BeOfType<StrongEmphasis>();
        }

        [Fact]
        public void Combined()
        {
            /* given */
            var text = new StringRange("emphasis *1234*, **1234**, link [title](link), image ![title](http)");
            var parser = new InlineParser();

            /* when */
            var result = parser.Parse(text).ToList();

            /* then */
            result.Should().HaveCount(12);
        }
    }
}