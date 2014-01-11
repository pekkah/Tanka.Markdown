namespace Tanka.MarkdownTests.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class EmphasisFactoryFacts
    {
        [Fact]
        public void ShouldMatchEmphasis()
        {
            // arrange
            var factory = new EmphasisFactory();

            // act & assert
            factory.IsMatch(new[] {new Token(TokenType.Emphasis)}).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldMatchStrongEmphasis()
        {
            // arrange
            var factory = new EmphasisFactory();

            // act & assert
            factory.IsMatch(new[] {new Token(TokenType.StrongEmphasis)}).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ShouldCreateEmphasis()
        {
            // arrange
            string markdown = "*some text*";
            var tokens = new Stack<Token>(new[]
            {
                new Token(TokenType.Emphasis, 0),
                new Token(TokenType.Text, 1),
                new Token(TokenType.Emphasis, markdown.LastIndexOf("*", StringComparison.Ordinal))
            }.Reverse());

            var factory = new EmphasisFactory();

            // act
            var emphasis = factory.Create(tokens, markdown) as EmphasisBeginOrEnd;

            // assert
            emphasis.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCreateStrongEmphasis()
        {
            // arrange
            string markdown = "**some text**";
            var tokens = new Stack<Token>(new[]
            {
                new Token(TokenType.StrongEmphasis, 0),
                new Token(TokenType.Text, 2),
                new Token(TokenType.StrongEmphasis, markdown.LastIndexOf("**", StringComparison.Ordinal))
            }.Reverse());

            var factory = new EmphasisFactory();

            // act
            var emphasis = factory.Create(tokens, markdown) as StrongEmphasisBeginOrEnd;

            // assert
            emphasis.Should().NotBeNull();
        }
    }
}