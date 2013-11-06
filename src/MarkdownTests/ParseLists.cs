namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParseLists : MarkdownParserFactsBase
    {
        [Fact]
        public void ListsWithItemsStartingWithStar()
        {
            var builder = new StringBuilder();
            builder.AppendLine("* item 1");
            builder.AppendLine("* item 2");
            builder.AppendLine("* item 3");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3
                }, l => l.Items))
                .BDDfy();
        }

        [Fact]
        public void ListsWithItemsStartingWithHyphen()
        {
            var builder = new StringBuilder();
            builder.AppendLine("- item 1");
            builder.AppendLine("- item 2");
            builder.AppendLine("- item 3");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3
                }, l => l.Items))
                .BDDfy();
        }

        [Fact]
        public void ListsWithItemsStartingWithNumber()
        {
            var builder = new StringBuilder();
            builder.AppendLine("1. item 1");
            builder.AppendLine("2. item 2");
            builder.AppendLine("3. item 3");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(builder.ToString()))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3
                }, l => l.Items))
                .BDDfy();
        }
    }
}