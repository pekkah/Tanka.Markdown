namespace Tanka.MarkdownTests.Text
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;
    using Xunit.Extensions;

    public class LinkSpanFactoryFacts
    {
        [Fact]
        public void ShouldMatchTokenPattern()
        {
            var tokens = new[]
            {
                TokenType.LinkTitleStart,
                TokenType.Text,
                TokenType.LinkTitleEnd,
                TokenType.LinkUrlStart,
                TokenType.Text,
                TokenType.LinkUrlEnd
            };

            var factory = new LinkSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldNotMatch()
        {
            var tokens = new[]
            {
                TokenType.LinkTitleStart,
                TokenType.LinkTitleStart,
                TokenType.LinkTitleEnd,
                TokenType.LinkUrlStart,
                TokenType.Text,
                TokenType.LinkUrlEnd
            };

            var factory = new LinkSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void ShouldCreateSpanFromTokens()
        {
            const string content = "[title](http://url)";
            var tokens = new Stack<Token>(new[]
            {
                new Token
                {
                    Type = TokenType.LinkTitleStart,
                    StartPosition = content.IndexOf('['),
                },
                new Token
                {
                    Type = TokenType.Text,
                    StartPosition = content.IndexOf("title", System.StringComparison.Ordinal),
                },
                new Token
                {
                    Type = TokenType.LinkTitleEnd,
                    StartPosition = content.IndexOf(']'),
                },
                new Token
                {
                    Type = TokenType.LinkUrlStart,
                    StartPosition = content.IndexOf('('),
                },
                new Token
                {
                    Type = TokenType.Text,
                    StartPosition = content.IndexOf("http", System.StringComparison.Ordinal),
                },
                new Token
                {
                    Type = TokenType.LinkUrlEnd,
                    StartPosition = content.IndexOf(')'),
                }
            }.Reverse());

            var factory = new LinkSpanFactory();
            var link = factory.Create(tokens, content).As<LinkSpan>();
            link.Title.ShouldBeEquivalentTo("title");
            link.UrlOrKey.ShouldBeEquivalentTo("http://url");
        }
    }
}