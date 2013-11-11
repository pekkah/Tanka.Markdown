namespace Tanka.MarkdownTests
{
    using System.IO;
    using System.Text;
    using Markdown.Blocks;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class SmokeTheDocument : MarkdownParserFactsBase
    {
        [Fact]
        public void SampleDocument()
        {
            var markdown = File.ReadAllText("TheDocument.txt");

            this.Given(t => t.GivenMarkdownParserWithDefaults())
                .And(t => t.GivenTheMarkdown(markdown))
                .When(t => t.WhenTheMarkdownIsParsed())
                .Then(t => t.ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 1,
                    Text = "The Document"
                }))
                .And(t => t.ThenDocumentChildAtIndexShouldMatch<Paragraph>(1, new
                {
                    Content = "This document starts with Setext style heading level one and continues with two level paragraph. This parahraph."
                }))
                .And(t => t.ThenListAtIndexShouldMatch(2,
                    "setext headings",
                    "parahraphs",
                    "lists",
                    "normal headings",
                    "code blocks"))
                .And(t => t.ThenDocumentChildAtIndexShouldMatch<Heading>(3, new
                {
                    Level = 2,
                    Text = "Sample code"
                }))
                .And(t => t.ThenDocumentChildAtIndexShouldMatch<Codeblock>(4, new
                {
                    Language = "javascript",
                    Code = "function() {\r\n	var hello = \"world\";\r\n}\r\n"
                }))
                .And(t => t.ThenDocumentChildAtIndexShouldBe(5, typeof(Blockquote)))
                .BDDfy();
        }
    }
}