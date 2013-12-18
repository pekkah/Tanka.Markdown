namespace Tanka.MarkdownTests
{
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Blocks;
    using Markdown.Text;
    using Xbehave;

    public class SmokeTheDocument : MarkdownParserFactsBase
    {
        [Scenario]
        public void SampleDocument()
        {
            string markdown = File.ReadAllText("TheDocument.txt");

            "Given markdown parser with defaults and the markdown content"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(markdown);
                });

            "When markdown content is parsed"
                .When(() => WhenTheMarkdownIsParsed());

            "Then should parse headings"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 1,
                    Text = "The Document"
                }));
            "And paragraphs"
                .And(() => ThenDocumentChildAtIndexShould<Paragraph>(1, p => p.Content.First().As<TextSpan>().Content
                    .ShouldBeEquivalentTo(
                        "This document starts with Setext style heading level one and continues with two level paragraph. This parahraph.")));

            "And lists"
                .And(() => ThenListAtIndexShouldMatch(2,
                    "setext headings",
                    "parahraphs",
                    "lists",
                    "normal headings",
                    "code blocks"));

            "And headings at different level"
                .And(() => ThenDocumentChildAtIndexShouldMatch<Heading>(3, new
                {
                    Level = 2,
                    Text = "Sample code"
                }));

            "And codeblocks"
                .And(() => ThenDocumentChildAtIndexShouldMatch<Codeblock>(4, new
                {
                    Language = "javascript",
                    Code = "function() {\r\n	var hello = \"world\";\r\n}\r\n"
                }));
            "And blockquotes"
                .And(() => ThenDocumentChildAtIndexShouldBe(5, typeof (Blockquote)));
        }
    }
}