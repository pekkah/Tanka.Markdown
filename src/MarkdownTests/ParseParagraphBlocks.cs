namespace Tanka.MarkdownTests
{
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Markdown.Blocks;
    using Markdown.Text;
    using Xbehave;

    public class ParseParagraphBlocks : MarkdownParserFactsBase
    {
        [Scenario]
        public void ParagraphWithOnlyEmphasis()
        {
            var builder = new StringBuilder();
            builder.AppendLine("*emphasis*");

            "Given markdown with emphasis"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should paragraph and content should be emphasis text emphasis"
                .Then(() => ThenDocumentChildAtIndexShould<Paragraph>(0, paragraph =>
                {
                    paragraph.Content.Should().HaveCount(3);
                    paragraph.Content.First().Should().BeOfType<EmphasisBeginOrEnd>();
                    paragraph.Content.ElementAt(1).Should().BeOfType<TextSpan>();
                    paragraph.Content.Last().Should().BeOfType<EmphasisBeginOrEnd>();
                }));
        }

        [Scenario]
        public void ParagraphWithOnlyStrongEmphasis()
        {
            var builder = new StringBuilder();
            builder.AppendLine("**emphasis**");

            "Given markdown with emphasis"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should paragraph and content should be strong emphasis text strong emphasis"
                .Then(() => ThenDocumentChildAtIndexShould<Paragraph>(0, paragraph =>
                {
                    paragraph.Content.Should().HaveCount(3);
                    paragraph.Content.First().Should().BeOfType<StrongEmphasisBeginOrEnd>();
                    paragraph.Content.ElementAt(1).Should().BeOfType<TextSpan>();
                    paragraph.Content.Last().Should().BeOfType<StrongEmphasisBeginOrEnd>();
                }));
        }

        [Scenario]
        public void ParagraphWithText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Some text here as the first line of the paragraph");
            builder.AppendLine("more text here as the second line of text");

            "Given markdown with paragraph of text"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should paragraph and content should be only textspan"
                .Then(() => ThenDocumentChildAtIndexShould<Paragraph>(0, paragraph =>
                {
                    paragraph.Content.Should().HaveCount(1);
                    paragraph.Content.Should().OnlyContain(span => span is TextSpan);
                }));
        }
    }
}