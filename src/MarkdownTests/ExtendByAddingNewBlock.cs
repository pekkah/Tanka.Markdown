namespace Tanka.MarkdownTests
{
    using System;
    using System.Text;
    using Markdown;
    using Markdown.Gist;
    using Xbehave;

    public class ExtendByAddingNewBlock : MarkdownParserFactsBase
    {
        [Scenario]
        public void GistBlock()
        {
            var builder = new StringBuilder();
            builder.AppendLine("https://gist.github.com/pekkah/8304465");

            "Given markdown parser with GistFactory"
                .Given(() =>
                {
                    var options = MarkdownParserOptions.Defaults;
                    options.BlockFactories.Insert(0, new GistFactory());

                    var parser = new MarkdownParser(options);

                    GivenMarkdownParser(parser);
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be Gist with gist id and user name"
                .And(() => ThenDocumentChildAtIndexShouldMatch<GistBlock>(0, new
                {
                    GistId = "8304465",
                    UserName = "pekkah"
                }));
        }

        
    }
}