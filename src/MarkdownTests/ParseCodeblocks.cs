namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown.Blocks;
    using Xbehave;
    using Xunit;

    public class ParseCodeblocks : MarkdownParserFactsBase
    {
        [Scenario]
        public void CodeblockWithLanguage()
        {
            var builder = new StringBuilder();
            builder.AppendLine("``` javascript");
            builder.AppendLine("function() { }");
            builder.AppendLine("```");

            "Given markdown with a codeblock"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });
                
                "When markdown is parsed"
                    .When(WhenTheMarkdownIsParsed);

            "Then document child at index 0 should be codeblock and match"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<Codeblock>(0, new
                {
                    Language = "javascript",
                    Code = "function() { }\r\n"
                }));
        }
    }
}