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
                new Token(TokenType.Text),
                new Token(TokenType.End)
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
        public void ShouldNotMatchWhenNotFirstInPattern(string typeOfFirstToken)
        {
            var tokens = new[]
            {
                new Token(typeOfFirstToken),
                new Token(TokenType.Text)
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
        [InlineData(TokenType.Image)]
        public void ShouldMatchTokenPatternWhenNextAny(string typeOfNextToken)
        {
            var tokens = new[]
            {
                new Token(TokenType.Text),
                new Token(typeOfNextToken)
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
        [InlineData(TokenType.Text)]
        public void ShouldCreateSpanWhenNexAny(string typeOfNextToken)
        {
            const string content = "1234567890";

            var tokens = new Stack<Token>(new[]
            {
                new Token(TokenType.Text, 0),
                new Token(typeOfNextToken, 10)
            }.Reverse());

            var factory = new TextSpanFactory();
            factory.Create(tokens, content).As<TextSpan>().Content.ShouldAllBeEquivalentTo(content);
        }

        [Fact]
        public void ShouldCreateSpanFromTokens()
        {
            const string content = "1234567890";
            var tokens = new Stack<Token>(new[]
            {
                new Token(TokenType.Text, 0),
                new Token(TokenType.End, 10)
            }.Reverse());

            var factory = new TextSpanFactory();
            factory.Create(tokens, content).As<TextSpan>().Content.ShouldAllBeEquivalentTo(content);
        }
    }
}