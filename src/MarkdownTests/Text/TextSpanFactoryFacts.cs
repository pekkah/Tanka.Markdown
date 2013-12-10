namespace Tanka.MarkdownTests.Text
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;
    using Xunit.Extensions;

    public class TextSpanFactoryFacts
    {
        [Fact]
        public void ShouldMatchTokenPatternWhenNextEnd()
        {
            var tokens = new[]
            {
                TokenType.Text,
                TokenType.End, 
            };

            var factory = new TextSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(true);
        }

        [Theory]
        [InlineData(TokenType.Emphasis)]
        [InlineData(TokenType.LinkTitleEnd)]
        [InlineData(TokenType.LinkTitleStart)]
        [InlineData(TokenType.LinkUrlEnd)]
        [InlineData(TokenType.LinkUrlStart)]
        public void ShouldNotMatchWhenNotFirstInPattern(TokenType typeOfFirstToken)
        {
            var tokens = new[]
            {
                typeOfFirstToken,
                TokenType.Text,
            };

            var factory = new TextSpanFactory();

            factory.IsMatch(tokens).ShouldBeEquivalentTo(false);
        }

        [Theory]
        [InlineData(TokenType.Emphasis)]
        [InlineData(TokenType.LinkTitleEnd)]
        [InlineData(TokenType.LinkTitleStart)]
        [InlineData(TokenType.LinkUrlEnd)]
        [InlineData(TokenType.LinkUrlStart)]
        [InlineData(TokenType.Text)]
        public void ShouldMatchTokenPatternWhenNextAny(TokenType typeOfNextToken)
        {
            var tokens = new[]
            {
                TokenType.Text,
                typeOfNextToken, 
            };

            var factory = new TextSpanFactory();
            factory.IsMatch(tokens).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldCreateSpanFromTokens()
        {
            const string content = "1234567890";
            var tokens = new Stack<Token>(new[]
            {
                new Token()
                {
                    Type = TokenType.Text,
                    StartPosition = 0,
                },
                new Token()
                {
                    Type = TokenType.End,
                    StartPosition = 10,
                }
            }.Reverse());

            var factory = new TextSpanFactory();
            factory.Create(tokens, content).As<TextSpan>().Content.ShouldAllBeEquivalentTo(content);
        }
    }
}