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

            var tokenizer = new StringTokenizer(text);
            IEnumerable<Token> result = tokenizer.Tokenize();

            result.Should().ContainSingle(t => t.Type == TokenType.Text && t.StartPosition == 0);
        }

        [Fact]
        public void TokenizeTextWithExlamationMark()
        {
            const string text = "Text some here!";

            var tokenizer = new StringTokenizer(text);
            IEnumerable<Token> result = tokenizer.Tokenize();

            result.Should().ContainSingle(t => t.Type == TokenType.Text && t.StartPosition == 0);
        }

        [Fact]
        public void TokenizeLink()
        {
            const string text = "[Google](http://google.fi)";

            var tokenizer = new StringTokenizer(text);
            IEnumerable<Token> result = tokenizer.Tokenize();

            result.Should().ContainInOrder(new[]
            {
                new Token
                {
                    StartPosition = 0,
                    Type = TokenType.LinkTitleStart
                },
                new Token
                {
                    StartPosition = 1,
                    Type = TokenType.Text
                },
                new Token
                {
                    StartPosition = 7,
                    Type = TokenType.LinkTitleEnd
                },
                new Token
                {
                    StartPosition = 8,
                    Type = TokenType.LinkUrlStart
                },
                new Token
                {
                    StartPosition = 9,
                    Type = TokenType.Text
                },
                new Token
                {
                    StartPosition = 25,
                    Type = TokenType.LinkUrlEnd
                }
            });
        }
    }
}