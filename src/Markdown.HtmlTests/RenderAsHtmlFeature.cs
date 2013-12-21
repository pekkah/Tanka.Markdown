namespace Markdown.HtmlTests
{
    using System.Text;
    using FluentAssertions;
    using Tanka.Markdown;
    using Tanka.Markdown.Html;
    using Xunit;

    public class RenderAsHtmlFeature
    {
        [Fact]
        public void RenderHeadings()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("# heading 1");
            markdown.AppendLine("## heading 2");
            markdown.AppendLine("### heading 3");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<h1>heading 1</h1>");
            expectedHtml.Append("<h2>heading 2</h2>");
            expectedHtml.Append("<h3>heading 3</h3>");

            var parser = new MarkdownParser();
            var renderer = new HtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderCodeblocks()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("``` csharp");
            markdown.AppendLine("string name = \"test\";");
            markdown.AppendLine("```");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<pre><code data-lang=\"csharp\">");
            expectedHtml.Append("string name = \"test\";");
            expectedHtml.Append("</code></pre>");

            var parser = new MarkdownParser();
            var renderer = new HtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderOrderedLists()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("1. item");
            markdown.AppendLine("2. item");
            markdown.AppendLine("3. item");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<ol>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("</ol>");

            var parser = new MarkdownParser();
            var renderer = new HtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderUnorderedLists()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("- item");
            markdown.AppendLine("- item");
            markdown.AppendLine("- item");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<ul>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("<li>item</li>");
            expectedHtml.Append("</ul>");

            var parser = new MarkdownParser();
            var renderer = new HtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }
    }
}