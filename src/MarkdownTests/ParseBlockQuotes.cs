namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown.Blocks;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParseBlockquotes : MarkdownParserFactsBase
    {
        [Fact]
        public void SingleLineOfQuotedText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("> Quoted one line of text");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildAtIndexShouldBe(0, typeof (Blockquote)))
                .BDDfy();
        }

        [Fact]
        public void MultipleLinesOfQuotedText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildAtIndexShouldBe(0, typeof (Blockquote)))
                .And(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .BDDfy();
        }
    }
}