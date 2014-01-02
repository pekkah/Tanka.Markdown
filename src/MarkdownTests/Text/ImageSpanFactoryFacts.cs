namespace Tanka.MarkdownTests.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class ImageSpanFactoryFacts
    {
        [Fact]
        public void ShouldMatchTokenPattern()
        {
            var tokens = new[]
            {
                TokenType.Image,
                TokenType.LinkTitleStart,
                TokenType.Text,
                TokenType.LinkTitleEnd,
                TokenType.LinkUrlStart,
                TokenType.Text,
                TokenType.LinkUrlEnd
            };

            var factory = new ImageSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldNotMatch()
        {
            var tokens = new[]
            {
                TokenType.Image,
                TokenType.LinkTitleStart,
                TokenType.LinkTitleStart,
                TokenType.LinkTitleEnd,
                TokenType.LinkUrlStart,
                TokenType.Text,
                TokenType.LinkUrlEnd
            };

            var factory = new ImageSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void ShouldCreateSpanFromTokens()
        {
            const string content = "![title](http://url)";
            var tokens = new Stack<Token>(new[]
            {
                new Token()
                {
                    Type = TokenType.Image,
                    StartPosition = content.IndexOf('!')
                }, 
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
                    Type = TokenType.LinkUrlStart,
                    StartPosition = content.IndexOf('('),
                },
                new Token
                {
                    Type = TokenType.Text,
                    StartPosition = content.IndexOf("http", StringComparison.Ordinal),
                },
                new Token
                {
                    Type = TokenType.LinkUrlEnd,
                    StartPosition = content.IndexOf(')'),
                }
            }.Reverse());

            var factory = new ImageSpanFactory();
            var link = factory.Create(tokens, content).As<ImageSpan>();
            link.AltText.ShouldBeEquivalentTo("title");
            link.UrlOrKey.ShouldBeEquivalentTo("http://url");
        }
    }
}