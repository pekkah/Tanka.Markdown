namespace Tanka.MarkdownTests
{
    using System;
    using System.Text;
    using FluentAssertions;
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
                    var parser = new MarkdownParser();
                    parser.Builders.Insert(0, new GistBuilder());

                    GivenMarkdownParser(parser);
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(()=>WhenTheMarkdownIsParsed());

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be Gist with gist id and user name"
                .And(() => ThenDocumentChildAtIndexShould<GistBlock>(0, gist =>
                {
                    gist.UserName.ToString().ShouldBeEquivalentTo("pekkah");
                    gist.GistId.ToString().ShouldBeEquivalentTo("8304465");
                }));
        }

        
    }
}