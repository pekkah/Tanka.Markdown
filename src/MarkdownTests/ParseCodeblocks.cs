namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown.Blocks;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParseCodeblocks : MarkdownParserFactsBase
    {
        [Fact]
        public void ListsWithItemsStartingWithStar()
        {
            var builder = new StringBuilder();
            builder.AppendLine("``` javascript");
            builder.AppendLine("function() { }");
            builder.AppendLine("```");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Codeblock>(0, new
                {
                    Language = "javascript",
                    Code = "function() { }"
                }))
                .BDDfy();
        }
    }
}