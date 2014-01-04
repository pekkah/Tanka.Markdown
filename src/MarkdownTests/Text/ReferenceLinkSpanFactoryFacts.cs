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
                TokenType.LinkTitleStart,
                TokenType.Text,
                TokenType.LinkTitleEnd,
                TokenType.LinkTitleStart,
                TokenType.Text,
                TokenType.LinkTitleEnd
            };

            var factory = new ReferenceLinkSpanFactory();

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
                {
                    Type = TokenType.LinkTitleStart,
                    StartPosition = content.IndexOf('['),
                },
                new Token
                {
                    Type = TokenType.Text,
                    StartPosition = content.IndexOf("title", StringComparison.Ordinal),
                },
                new Token
                {
                    Type = TokenType.LinkTitleEnd,
                    StartPosition = content.IndexOf(']'),
                },
                new Token
                {
                    Type = TokenType.LinkTitleStart,
                    StartPosition = content.LastIndexOf('['),
                },
                new Token
                {
                    Type = TokenType.Text,
                    StartPosition = content.IndexOf("1", StringComparison.Ordinal),
                },
                new Token
                {
                    Type = TokenType.LinkTitleEnd,
                    StartPosition = content.LastIndexOf(']'),
                }
            }.Reverse());

            var factory = new ReferenceLinkSpanFactory();
            var link = factory.Create(tokens, content).As<LinkSpan>();
            link.Title.ShouldBeEquivalentTo("title");
            link.UrlOrKey.ShouldBeEquivalentTo("1");
            link.IsKey.ShouldBeEquivalentTo(true);
        }
    }
}