namespace Tanka.MarkdownTests
{
    using System;
    using System.Text;
    using Markdown.Blocks;
    using Xbehave;

    public class ParseHeadingBlocks : MarkdownParserFactsBase
    {
        [Scenario]
        public void H1_blocks_starting_with_hash()
        {
            const string markdown = "# Heading 1";

            "Given markdown with a level one heading with hash"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(markdown);
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 1,
                    Text = "Heading 1"
                }));
        }

        [Scenario]
        public void H2_blocks_starting_with_hash()
        {
            const string markdown = "## Heading";

            "Given markdown with a level two heading with hash"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(markdown);
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 2,
                    Text = "Heading"
                }));
        }

        [Scenario]
        public void H1andH2_blocks_starting_with_hash()
        {
            string markdown = "## First" + Environment.NewLine + "# Second";

            "Given markdown with second level heading and first level heading one separate lines"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(markdown);
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 2,
                    Text = "First"
                }));

            "And child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(1, new
                {
                    Level = 1,
                    Text = "Second"
                }));
        }

        [Scenario]
        public void Setext_level_one_headings()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Heading text");
            builder.AppendLine("============");

            "Given markdown with a setext level one heading"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 1,
                    Text = "Heading text"
                }));
        }

        [Scenario]
        public void Setext_level_two_headings()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Heading text");
            builder.AppendLine("-----------");

            "Given markdown with a settext level two heading"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then child at index 0 should be heading and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Heading>(0, new
                {
                    Level = 2,
                    Text = "Heading text"
                }));
        }
    }
}