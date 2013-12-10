namespace Tanka.MarkdownTests.Blocks
{
    using System;
    using System.Text;
    using Markdown.Blocks;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParagraphFacts : MarkdownParserFactsBase
    {
        [Fact]
        public void SingleLineOfText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("first line");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Paragraph>(0, new
                {
                    Content = "first line"
                }))
                .BDDfy();
        }

        [Fact]
        public void MultipleLinesOfText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("first line");
            builder.AppendLine("second line of text here");

            var text = builder.ToString();
            var expectedText = text.Replace(Environment.NewLine, " ").TrimEnd();

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(text))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Paragraph>(0, new
                {
                    Content = expectedText
                }))
                .BDDfy();
        }
    }
}