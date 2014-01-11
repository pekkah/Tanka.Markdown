namespace Tanka.MarkdownTests.Text
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Markdown.Text;
    using Xunit;

    public class StringTokenizerFacts
    {
        [Fact]
        public void TokenizeText()
        {
            const string text = "Text some here";

            var tokenizer = new StringTokenizer();
            IEnumerable<Token> result = tokenizer.Tokenize(text);

            result.Should().ContainSingle(t => t.Type == TokenType.Text && t.StartPosition == 0);
        }

        [Fact]
        public void TokenizeTextWithExlamationMark()
        {
            const string text = "Text some here!";

            var tokenizer = new StringTokenizer();
            IEnumerable<Token> result = tokenizer.Tokenize(text);

            result.Should().ContainSingle(t => t.Type == TokenType.Text && t.StartPosition == 0);
        }

        [Fact]
        public void TokenizeLink()
        {
            const string text = "[Google](http://google.fi)";

            var tokenizer = new StringTokenizer();
            IEnumerable<Token> result = tokenizer.Tokenize(text);

            result.Should().ContainInOrder(new[]
            {
                new Token(TokenType.LinkTitleStart, 0),
                new Token(TokenType.Text, 1),
                new Token(TokenType.LinkTitleEnd, 7),
                new Token(TokenType.LinkUrlStart, 8),
                new Token(TokenType.Text, 9),
                new Token(TokenType.LinkUrlEnd, 25)
            });
        }

        [Fact]
        public void TokenizeEmphasis()
        {
            // arrange
            const string text = "*";
            var tokenizer = new StringTokenizer();

            // act
            var result = tokenizer.Tokenize(text);

            // assert
            result.Should().ContainSingle(t => t.Type == TokenType.Emphasis);
        }


        [Fact]
        public void TokenizeDoubleEmphasis()
        {
            // arrange
            const string text = "**";
            var tokenizer = new StringTokenizer();

            // act
            var result = tokenizer.Tokenize(text);

            // assert
            result.Should().ContainSingle(t => t.Type == TokenType.StrongEmphasis);
        }
    }
}