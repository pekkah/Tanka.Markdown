namespace Tanka.MarkdownTests.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class LinkSpanFactoryFacts
    {
        [Fact]
        public void ShouldMatchTokenPattern()
        {
            var tokens = new[]
            {
                new Token(TokenType.LinkTitleStart),
                new Token(TokenType.Text),
                new Token(TokenType.LinkTitleEnd),
                new Token(TokenType.LinkUrlStart),
                new Token(TokenType.Text),
                new Token(TokenType.LinkUrlEnd)
            };

            var factory = new LinkSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldNotMatch()
        {
            var tokens = new[]
            {
                new Token(TokenType.LinkTitleStart),
                new Token(TokenType.LinkTitleStart),
                new Token(TokenType.LinkTitleEnd),
                new Token(TokenType.LinkUrlStart),
                new Token(TokenType.Text),
                new Token(TokenType.LinkUrlEnd)
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
                (
                    TokenType.LinkTitleStart,
                    content.IndexOf('[')
                ),
                new Token
                (
                    TokenType.Text,
                    content.IndexOf("title", StringComparison.Ordinal)
                ),
                new Token
                (
                    TokenType.LinkTitleEnd,
                    content.IndexOf(']')
                ),
                new Token
                (
                    TokenType.LinkUrlStart,
                    content.IndexOf('(')
                ),
                new Token
                (
                    TokenType.Text,
                    content.IndexOf("http", StringComparison.Ordinal)
                ),
                new Token
                (
                    TokenType.LinkUrlEnd,
                    content.IndexOf(')')
                )
            }.Reverse());

            var factory = new LinkSpanFactory();
            var link = factory.Create(tokens, content).As<LinkSpan>();
            link.Title.ShouldBeEquivalentTo("title");
            link.UrlOrKey.ShouldBeEquivalentTo("http://url");
        }
    }
}