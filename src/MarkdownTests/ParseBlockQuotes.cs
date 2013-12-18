namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown.Blocks;
    using Xbehave;

    public class ParseBlockquotes : MarkdownParserFactsBase
    {
        [Scenario]
        public void SingleLineOfQuotedText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("> Quoted one line of text");

            "Given markdown with a single line block quote"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then document child at index 0 should be blockquote"
                .Then(() => ThenDocumentChildAtIndexShouldBe(0, typeof (Blockquote)));
        }

        [Scenario]
        public void MultipleLinesOfQuotedText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");
            builder.AppendLine("> Quoted line of text");

            "Given markdown with a single line block quote"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdwn is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then document child at index 0 should be blockquote"
                .Then(() => ThenDocumentChildAtIndexShouldBe(0, typeof (Blockquote)));
        }
    }
}