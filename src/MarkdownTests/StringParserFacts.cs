namespace Tanka.MarkdownTests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class StringParserFacts
    {
        private readonly Queue<char?> _readChars = new Queue<char?>();
        private readonly Queue<string> _readStrings = new Queue<string>();
        private StringParser _parser;

        [Fact]
        public void reading_a_character()
        {
            const string text = "lorem ipsum";
            this.Given(_ => _.GivenParserWithString(text))
                .When(_ => _.WhenOneCharacterRead())
                .Then(_ => _.ThenReadCharShouldBe('l'))
                .BDDfy();
        }

        [Fact]
        public void reading_characters_in_sequence()
        {
            const string text = "lorem ipsum";
            this.Given(_ => _.GivenParserWithString(text))
                .When(_ => _.WhenOneCharacterRead())
                .And(_ => _.WhenOneCharacterRead())
                .Then(_ => _.ThenReadCharShouldBe('l'))
                .And(_ => _.ThenReadCharShouldBe('o'))
                .BDDfy();
        }

        [Fact]
        public void reading_until_character_found()
        {
            const string text = "lorem ipsum";
            this.Given(_ => _.GivenParserWithString(text))
                .When(_ => _.WhenReadingUntilChar(' '))
                .Then(_ => _.ThenReadStringShouldBe("lorem"))
                .BDDfy();
        }

        [Fact]
        public void reading_until_empty_line()
        {
            string text = "lorem ipsum" + Environment.NewLine + Environment.NewLine;
            this.Given(_ => _.GivenParserWithString(text))
                .When(_ => _.WhenReadingUntilEmptyLine())
                .Then(_ => _.ThenReadStringShouldBe("lorem ipsum"))
                .BDDfy();
        }

        private void GivenParserWithString(string text)
        {
            _parser = new StringParser(text);
        }

        private void WhenOneCharacterRead()
        {
            _readChars.Enqueue(_parser.ReadOne());
        }

        private void ThenReadCharShouldBe(char expected)
        {
            char? readChar = _readChars.Dequeue();
            readChar.ShouldBeEquivalentTo(expected);
        }

        private void WhenReadingUntilChar(char c)
        {
            _readStrings.Enqueue(_parser.ReadUntil(c));
        }

        private void ThenReadStringShouldBe(string str)
        {
            var readStr = _readStrings.Dequeue();
            readStr.ShouldAllBeEquivalentTo(str);
        }

        private void WhenReadingUntilEmptyLine()
        {
           _readStrings.Enqueue(_parser.ReadUntilEmptyLine());
        }
    }
}