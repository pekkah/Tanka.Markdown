namespace Tanka.MarkdownTests
{
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using TestStack.BDDfy;
    using TestStack.BDDfy.Scanners.StepScanners.Fluent;
    using Xunit;

    public class ParagraphFactoryFacts
    {
        private string _current;
        private bool _isMatch;
        private string _next;

        [Fact]
        public void WithJustCurrent()
        {
            const string currentLine = "lorem ipsum of some text here";
            const string nextLine = null;

            this.Given(_ => _.GivenLines(currentLine, nextLine))
                .When(_ => _.WhenMatched())
                .Then(_ => _.ThenShouldMatch())
                .BDDfy();
        }

        [Fact]
        public void MultipleLinesOfText()
        {
            const string currentLine = "Current line";
            const string nextLine = "Next line";

            this.Given(_ => _.GivenLines(currentLine, nextLine))
                .When(_ => _.WhenMatched())
                .Then(_ => _.ThenShouldMatch())
                .BDDfy();
        }

        [Fact]
        public void SetextHeadingsAreNotParagraphs()
        {
            const string currentLine = "Current line";
            const string nextLine = "=======";

            this.Given(_ => _.GivenLines(currentLine, nextLine))
                .When(_ => _.WhenMatched())
                .Then(_ => _.ThenShouldNotMatch())
                .BDDfy();
        }

        private void GivenLines(string current, string next)
        {
            _current = current;
            _next = next;
        }

        private void WhenMatched()
        {
            var factory = new ParagraphFactory();
            _isMatch = factory.IsMatch(_current, _next);
        }

        private void ThenShouldMatch()
        {
            _isMatch.ShouldBeEquivalentTo(true);
        }

        private void ThenShouldNotMatch()
        {
            _isMatch.ShouldBeEquivalentTo(false);
        }
    }
}