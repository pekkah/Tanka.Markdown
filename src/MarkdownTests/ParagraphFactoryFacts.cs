namespace Tanka.MarkdownTests
{
    using FluentAssertions;
    using Markdown.Blocks;
    using Xbehave;
    using Xunit;

    public class ParagraphFactoryFacts
    {

        [Scenario]
        public void WithJustCurrent()
        {
            const string currentLine = "lorem ipsum of some text here";
            const string nextLine = null;

            ParagraphFactory factory = null;

            "Given paragraph factory"
                .Given(() => factory = new ParagraphFactory());

            bool isMatch = false;

            "When matching with line of text and next line empty"
                .When(() => isMatch = factory.IsMatch(currentLine, nextLine));

            "Then should match"
                .Then(() => isMatch.ShouldBeEquivalentTo(true));

        }

        [Scenario]
        public void MultipleLinesOfText()
        {
            const string currentLine = "Current line";
            const string nextLine = "Next line";

            ParagraphFactory factory = null;

            "Given paragraph factory"
                .Given(() => factory = new ParagraphFactory());

            bool isMatch = false;

            "When matching with line of text and next line empty"
                .When(() => isMatch = factory.IsMatch(currentLine, nextLine));

            "Then should match"
                .Then(() => isMatch.ShouldBeEquivalentTo(true));
        }
    }
}