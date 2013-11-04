namespace Tanka.MarkdownTests
{
    using System;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParseHeadingBlocks : MarkdownParserFactsBase
    {
        [Fact]
        public void H1_blocks_starting_with_hash()
        {
            const string markdown = "# Heading 1";

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(markdown))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 1,
                    Text = "Heading 1"
                }))
                .BDDfy();
        }

        [Fact]
        public void H2_blocks_starting_with_hash()
        {
            const string markdown = "## Heading";

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(markdown))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(1))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 2,
                    Text = "Heading"
                }))
                .BDDfy();
        }

        [Fact]
        public void H1andH2_blocks_starting_with_hash()
        {
            string markdown = "## First" + Environment.NewLine + "# Second";

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(markdown))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildrenShouldHaveCount(2))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 2,
                    Text = "First"
                }))
                .And(t => ThenDocumentChildAtIndexShouldMatch<Heading>(1, new
                {
                    Level = 1,
                    Text = "Second"
                }))
                .BDDfy();
        }
    }
}