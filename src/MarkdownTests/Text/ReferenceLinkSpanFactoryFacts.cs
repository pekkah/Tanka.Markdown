namespace Tanka.MarkdownTests.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class ReferenceLinkSpanFactoryFacts
    {
        [Fact]
        public void ShouldMatchTokenPattern()
        {
            var tokens = new[]
            {
                new Token(TokenType.LinkTitleStart),
                new Token(TokenType.Text),
                new Token(TokenType.LinkTitleEnd),
                new Token(TokenType.LinkTitleStart),
                new Token(TokenType.Text),
                new Token(TokenType.LinkTitleEnd)
            };

            var factory = new ReferenceLinkSpanFactory();

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

            var factory = new ReferenceLinkSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void ShouldCreateSpanFromTokens()
        {
            const string content = "[title][1]";
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
                    TokenType.LinkTitleStart,
                    content.LastIndexOf('[')
                    ),
                new Token
                    (
                    TokenType.Text,
                    content.IndexOf("1", StringComparison.Ordinal)
                    ),
                new Token
                    (
                    TokenType.LinkTitleEnd,
                    content.LastIndexOf(']')
                    )
            }.Reverse());

            var factory = new ReferenceLinkSpanFactory();
            var link = factory.Create(tokens, content).As<LinkSpan>();
            link.Title.ShouldBeEquivalentTo("title");
            link.UrlOrKey.ShouldBeEquivalentTo("1");
            link.IsKey.ShouldBeEquivalentTo(true);
        }
    }
}