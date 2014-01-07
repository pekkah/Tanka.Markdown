namespace Tanka.MarkdownTests.Blocks
{
    using FluentAssertions;
    using Markdown.Blocks;
    using Xunit;

    public class LinkDefinitionBlockFacts
    {
        [Fact]
        public void ParseKeyAndLink()
        {
            var markdown = "[100]: http://some-url.com";

            var builder = new LinkDefinitionBuilder();
            builder.AddLine(markdown);
            builder.End();

            var linkDefinition = builder.Create() as LinkDefinition;
            linkDefinition.Key.ShouldBeEquivalentTo("100");
            linkDefinition.Url.ShouldBeEquivalentTo("http://some-url.com");
        }

        [Fact]
        public void ParseKeyAndLinkAndIgnoreWhitespaceAtStart()
        {
            var markdown = "  [100]: http://some-url.com";

            var builder = new LinkDefinitionBuilder();
            builder.AddLine(markdown);
            builder.End();

            var linkDefinition = builder.Create() as LinkDefinition;
            linkDefinition.Key.ShouldBeEquivalentTo("100");
            linkDefinition.Url.ShouldBeEquivalentTo("http://some-url.com");
        }

        [Fact]
        public void MatchToLinkDefinition()
        {
            const string markdown = "[100]: http://some-url.com";

            var factory = new LinkDefinitionBuilderFactory();

            factory.IsMatch(markdown, null).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void MatchToLinkDefinitionAndIgnoreWhitespaceAtStart()
        {
            const string markdown = "  [100]: http://some-url.com";

            var factory = new LinkDefinitionBuilderFactory();

            factory.IsMatch(markdown, null).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void DoNotMatchToRandom()
        {
            const string markdown = "[100 http://some-url.com";

            var factory = new LinkDefinitionBuilderFactory();

            factory.IsMatch(markdown, null).ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void CreateBuilder()
        {
            var factory = new LinkDefinitionBuilderFactory();
            factory.Create().Should().BeOfType<LinkDefinitionBuilder>();
        }
    }
}