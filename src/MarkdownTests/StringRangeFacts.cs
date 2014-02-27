namespace Tanka.MarkdownTests
{
    using System;
    using FluentAssertions;
    using Markdown;
    using Xunit;

    public class StringRangeFacts
    {
        [Fact]
        public void Length2()
        {
            /* given */
            var stringRange = new StringRange("01");

            /* when */
            int length = stringRange.Length;

            /* then */
            length.ShouldBeEquivalentTo(2);
        }

        [Fact]
        public void Length4()
        {
            /* given */
            const string text = "test";

            /* when */
            var stringRange = new StringRange(text);
            int length = stringRange.Length;
            /* then */
            length.ShouldBeEquivalentTo(4);
        }

        [Fact]
        public void IndexOf()
        {
            /* given */
            const string text = "0123456789";
            var stringRange = new StringRange(text, 2, 7);

            /* when */
            var indexOfFour = stringRange.IndexOf('4');

            /* then */
            indexOfFour.ShouldBeEquivalentTo(4);
        }

        [Fact]
        public void IndexOfBeforeStart()
        {
            /* given */
            const string text = "0123456789";
            var stringRange = new StringRange(text, 2, 7);

            /* when */
            /* then */
            stringRange.IndexOf('1').ShouldBeEquivalentTo(-1);
        }

        [Fact]
        public void IndexOfAfterEnd()
        {
            /* given */
            const string text = "0123456789";
            var stringRange = new StringRange(text, 2, 7);

            /* when */
            /* then */
            stringRange.IndexOf('8').ShouldBeEquivalentTo(-1);
        }

        [Fact]
        public void IsStartOfLine_ContentStart()
        {
            /* given */
            const string text = "0123456789";
            var stringRange = new StringRange(text);

            /* when */
            /* then */
            stringRange.IsStartOfLine(0).ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void IsStartOfLine_StartOfLine()
        {
            /* given */
            const string text = "0123456789\nSecond line";
            var stringRange = new StringRange(text);

            /* when */
            /* then */
            stringRange.IsStartOfLine(stringRange.StartOfNextLine(0)).ShouldBeEquivalentTo(true);
        }
    }
}