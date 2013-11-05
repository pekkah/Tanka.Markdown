namespace Tanka.MarkdownTests
{
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class LineReaderFacts
    {
        private readonly Queue<string> _readQueue = new Queue<string>();
        private LineReader _reader;

        [Fact]
        public void ReadOneLine()
        {
            const string text = "lorem ipsum hic hic hic";

            this.Given(_ => _.GivenText(text))
                .When(_ => _.WhenReadLine())
                .Then(_ => _.ThenLineShouldBe("lorem ipsum hic hic hic"))
                .And(_ => _.ThenCurrentLineNumberShouldBe(1))
                .BDDfy();
        }

        [Fact]
        public void ReadMultipleLines()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");
            builder.AppendLine("more beer!");

            this.Given(_ => _.GivenText(builder.ToString()))
                .When(_ => _.WhenReadLines(3))
                .Then(_ => _.ThenLineShouldBe("lorem ipsum"))
                .And(_ => _.ThenLineShouldBe("hic hic hic"))
                .And(_ => _.ThenLineShouldBe("more beer!"))
                .And(_ => _.ThenCurrentLineNumberShouldBe(3))
                .BDDfy();
        }

        [Fact]
        public void PeekLine()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");

            this.Given(_ => _.GivenText(builder.ToString()))
                .When(_ => _.WhenPeekLine())
                .Then(_ => _.ThenLineShouldBe("lorem ipsum"))
                .And(_ => _.ThenCurrentLineNumberShouldBe(0))
                .BDDfy();
        }

        [Fact]
        public void PeekLineAfterReadLine()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("hic hic hic");

            this.Given(_ => _.GivenText(builder.ToString()))
                .When(_ => _.WhenReadLine())
                .And(_ => _.WhenPeekLine())
                .Then(_ => _.ThenLineShouldBe("lorem ipsum"))
                .And(_ => _.ThenLineShouldBe("hic hic hic"))
                .And(_ => _.ThenCurrentLineNumberShouldBe(1))
                .BDDfy();
        }

        [Fact]
        public void EndOfDocument()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");

            this.Given(_ => _.GivenText(builder.ToString()))
                .When(_ => _.WhenReadLine())
                .Then(_ => _.ThenShouldBeEndOfDocument())
                .BDDfy();
        }

        [Fact]
        public void EndOfDocumentWithMultipleLinesInDocument()
        {
            var builder = new StringBuilder();
            builder.AppendLine("lorem ipsum");
            builder.AppendLine("second line");
            builder.AppendLine("third line");

            this.Given(_ => _.GivenText(builder.ToString()))
                .When(_ => _.WhenReadLine())
                .And(_ => _.WhenReadLine())
                .And(_ => _.WhenReadLine())
                .Then(_ => _.ThenShouldBeEndOfDocument())
                .BDDfy();
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