namespace Tanka.MarkdownTests
{
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Xbehave;
    using Xunit;

    public class LineReaderFacts
    {
        private readonly Queue<string> _readQueue = new Queue<string>();
        private LineReader _reader;

        [Scenario]
        public void ReadOneLine()
        {
            const string text = "lorem ipsum hic hic hic";

            "Given line of text"
                .Given(() => GivenText(text));

            "When reading line"
                .When(WhenReadLine);

            "Then line should be"
                .Then(() => ThenLineShouldBe("lorem ipsum hic hic hic"));

            "And current line number should be 1"
                .And(() => ThenCurrentLineNumberShouldBe(1));
        }

        [Scenario]
        public void ReadMultipleLines()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");
            builder.AppendLine("more beer!");

            "Given lines of text"
                .Given(() => GivenText(builder.ToString()));

            "When reading three lines"
                .When(() => WhenReadLines(3));

            "Then 1st line should be"
                .Then(() => ThenLineShouldBe("lorem ipsum"));

            "Then 2nd line should be"
                .And(() => ThenLineShouldBe("hic hic hic"));

            "Then 3rd line should be"
                .And(() => ThenLineShouldBe("more beer!"));

            "And current line number should be 3"
                .And(() => ThenCurrentLineNumberShouldBe(3));
        }

        [Scenario]
        public void PeekLine()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");

            "Given two lines of text"
                .Given(() => GivenText(builder.ToString()));

            "When peeking a line"
                .When(WhenPeekLine);

            "Then line should be"
                .Then(() => ThenLineShouldBe("lorem ipsum"));

            "And current line should not change"
                .And(() => ThenCurrentLineNumberShouldBe(0));
        }

        [Scenario]
        public void PeekLineAfterReadLine()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");

            "Given two lines of text"
                .Given(() => GivenText(builder.ToString()));

            "When reading and peeking a line"
                .When(() =>
                {
                    WhenReadLine();
                    WhenPeekLine();
                });

            "Then 1st line should be"
                .Then(() => ThenLineShouldBe("lorem ipsum"));

            "And 2nd line should be"
                .And(() => ThenLineShouldBe("hic hic hic"));

            "And current line number should be one (peeking does not count)"
                .And(() => ThenCurrentLineNumberShouldBe(1));
        }

        [Scenario]
        public void EndOfDocument()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");

            "Given line of text"
                .Given(() => GivenText(builder.ToString()));

            "When line is read"
                .When(() => WhenReadLine());

            "Then should be at end of document"
                .Then(() => ThenShouldBeEndOfDocument());
        }

        [Scenario]
        public void EndOfDocumentWithMultipleLinesInDocument()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("second line");
            builder.AppendLine("third line");

            "Given three lines of text"
                .Given(() => GivenText(builder.ToString()));

            "When reading line"
                .When(() => WhenReadLine());

            "And another line"
                .And(() => WhenReadLine());

            "And one more"
                .And(() => WhenReadLine());

            "Then should be at end of document"
                .Then(() => ThenShouldBeEndOfDocument());
        }

        private void GivenText(string document)
        {
            _reader = new LineReader(document);
        }

        private void WhenReadLine()
        {
            _readQueue.Enqueue(_reader.ReadLine());
        }

        private void WhenPeekLine()
        {
            _readQueue.Enqueue(_reader.PeekLine());
        }

        private void WhenReadLines(int count)
        {
            IEnumerable<string> lines = _reader.ReadLines(count);

            foreach (string line in lines)
            {
                _readQueue.Enqueue(line);
            }
        }

        private void ThenLineShouldBe(string expectedLine)
        {
            string actualLine = _readQueue.Dequeue();

            actualLine.ShouldBeEquivalentTo(expectedLine);
        }

        private void ThenCurrentLineNumberShouldBe(int lineNumber)
        {
            _reader.CurrentLineNumber.ShouldBeEquivalentTo(lineNumber);
        }

        private void ThenShouldBeEndOfDocument()
        {
            _reader.EndOfDocument.ShouldBeEquivalentTo(true);
        }
    }
}