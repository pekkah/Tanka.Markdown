namespace Tanka.MarkdownTests
{
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Blocks;
    using Markdown.Inline;
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
                .Then(() => ThenDocumentChildAtIndexShould<Heading>(0, heading =>
                {
                    heading.Level.ShouldBeEquivalentTo(1);
                    heading.ToString().ShouldBeEquivalentTo("The Document");
                }));
            "And paragraphs"
                .And(() => ThenDocumentChildAtIndexShould<Paragraph>(
                    1,
                    p =>
                    {
                        p.Spans.First().As<TextSpan>()
                            .ToString()
                            .ShouldBeEquivalentTo(
                                "This document starts with Setext style heading level one and");

                        p.Spans.Last().As<TextSpan>()
                            .ToString()
                            .ShouldBeEquivalentTo("continues with two level paragraph. This parahraph.");
                    }));

            "And lists"
                .And(() => ThenListAtIndexShouldMatch(2,
                    "setext headings",
                    "parahraphs",
                    "lists",
                    "normal headings",
                    "code blocks"));

            "And headings at different level"
                .And(() => ThenDocumentChildAtIndexShould<Heading>(3, heading =>
                {
                    heading.Level.ShouldBeEquivalentTo(2);
                    heading.ToString().ShouldBeEquivalentTo("Sample code");
                }));

            //"And codeblocks"
            //    .And(() => ThenDocumentChildAtIndexShould<Codeblock>(4, code =>
            //    {
            //        string expected = "function() { var hello = \"world\";}";
            //        code.ToString()
            //            .Replace("\n", string.Empty)
            //            .Replace("\r", string.Empty)
            //            .ShouldBeEquivalentTo(expected);
            //    }));

            //"And blockquotes"
            //    .And(() => ThenDocumentChildAtIndexShouldBe(5, typeof (Blockquote)));

            "And images"
                .And(
                    () =>
                        ThenDocumentChildAtIndexShould<Paragraph>(6, p =>
                        {
                            var span = p.Spans.Last().As<ImageSpan>();
                            span.Title.ToString().ShouldBeEquivalentTo("alt");
                            span.Url.ToString().ShouldBeEquivalentTo("http://image.jpg");
                        }));

            "And reference link should be resolved"
                .And(() => ThenDocumentChildAtIndexShould<Paragraph>(7,
                    p =>
                    {
                        var linkSpan = p.Spans.Last().As<LinkSpan>();
                        linkSpan.Title.ToString().ShouldBeEquivalentTo("github");
                    }));

            "And link definition with key and url"
                .And(() => ThenDocumentChildAtIndexShould<LinkDefinitionList>(8, list =>
                {
                    var ld = list.Definitions.Single();
                    ld.Key.ToString().ShouldBeEquivalentTo("1");
                    ld.Url.ToString().ShouldBeEquivalentTo("https://github.com");
                }));
        }
    }
}