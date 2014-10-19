namespace Tanka.MarkdownTests
{
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Xunit;

    public class NormalizationFacts
    {
        [Fact]
        public void ProcessNewLines()
        {
            /* arrange */
            var markdownBuilder = new StringBuilder();
            markdownBuilder.AppendLine("# content");
            markdownBuilder.Append("something here \n");
            markdownBuilder.Append("more more \r\n");

            var expectedMarkdown = new StringBuilder();
            expectedMarkdown.Append("# content\n");
            expectedMarkdown.Append("something here \n");
            expectedMarkdown.Append("more more \n");

            var normalize = new Normalize();
            /* act */
            var actual = normalize.Process(markdownBuilder.ToString());

            /* assert */
            actual.ShouldBeEquivalentTo(expectedMarkdown.ToString());
        }
    }
}