namespace Tanka.MarkdownTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParseHeadingBlocks
    {
        private MarkdownParser _parser;
        private string _markdown;

        public MarkdownDocument Document { get; set; }

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

        private void ThenDocumentChildAtIndexShouldMatch<T>(int index, object expected) where T: Block
        {
            var child = Document.Blocks.ElementAtOrDefault(index);
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof(T).FullName, index);

            child.ShouldHave().AllRuntimeProperties().EqualTo(expected);
        }

        private void ThenDocumentChildrenShouldHaveCount(int count)
        {
            Document.Blocks.Should().HaveCount(count);
        }

        private void GivenMarkdownParserWithDefaults()
        {
            _parser = new MarkdownParser();                
        }

        private void GivenTheMarkdown(string markdown)
        {
            _markdown = markdown;
        }

        private void WhenTheMarkdownIsParsed()
        {
            Document = _parser.Parse(_markdown);
        }
    }
}