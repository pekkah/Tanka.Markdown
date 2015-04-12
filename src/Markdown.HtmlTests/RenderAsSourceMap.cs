namespace Markdown.HtmlTests
{
    using System.Text;
    using FluentAssertions;
    using Tanka.Markdown;
    using Tanka.Markdown.Html;
    using Xunit;

    public class RenderAsSourceMapFeature
    {
        [Fact]
        public void RenderMap()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("# heading 1");
            markdown.AppendLine("some content here");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<h1>heading 1</h1>");
            expectedHtml.Append("<p>some content here</p>");

            var expectedSourceMap = new StringBuilder();

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string[] outputs = renderer.RenderMultiple(document);

            /* assert */
            outputs.Length.ShouldBeEquivalentTo(2);

            // first output should be the html
            outputs[0].ShouldBeEquivalentTo(expectedHtml.ToString());

            // second output should be the source map
            outputs[1].ShouldBeEquivalentTo(expectedSourceMap.ToString());
        }
    }
}