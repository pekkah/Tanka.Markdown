namespace Tanka.MarkdownTests.Blocks
{
    using System;
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Markdown.Blocks;
    using Markdown.Text;
    using Xunit;

    public class ParagraphFacts : MarkdownParserFactsBase
    {
        [Fact]
        public void SingleLineOfText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("first line");

            GivenMarkdownParserWithDefaults();
            GivenTheMarkdown(builder.ToString());

            WhenTheMarkdownIsParsed();

            ThenDocumentChildrenShouldHaveCount(1);
            ThenDocumentChildAtIndexShould<Paragraph>(
                0,
                p => p.Content.Single().As<TextSpan>().Content.ShouldBeEquivalentTo("first line"));
        }

        [Fact]
        public void MultipleLinesOfText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("first line");
            builder.AppendLine("second line of text here");

            var text = builder.ToString();
            var expectedText = text.Replace(Environment.NewLine, " ").TrimEnd();

            GivenMarkdownParserWithDefaults();
            GivenTheMarkdown(text);
            WhenTheMarkdownIsParsed();
            ThenDocumentChildrenShouldHaveCount(1);
            ThenDocumentChildAtIndexShould<Paragraph>(
                0,
                p => p.Content.Single().As<TextSpan>().Content.ShouldBeEquivalentTo(expectedText));
        }
    }
}