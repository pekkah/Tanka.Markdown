namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParagraphFacts: MarkdownParserFactsBase
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
            
        }
    }
}