namespace Markdown.HtmlTests
{
    using System;
    using System.Text;
    using FluentAssertions;
    using Tanka.Markdown;
    using Tanka.Markdown.Blocks;
    using Tanka.Markdown.Html;
    using Xunit;

    public partial class RenderAsHtmlFeature
    {
        [Fact]
        public void RenderHeadings()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("# heading 1");
            markdown.AppendLine("## heading 2");
            markdown.AppendLine("### heading 3");
            markdown.AppendLine("Heading");
            markdown.AppendLine("=======");
            markdown.AppendLine("Heading");
            markdown.AppendLine("-------");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<h1>heading 1</h1>");
            expectedHtml.Append("<h2>heading 2</h2>");
            expectedHtml.Append("<h3>heading 3</h3>");
            expectedHtml.Append("<h1>Heading</h1>");
            expectedHtml.Append("<h2>Heading</h2>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

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
            markdown.Append("```\n");
            markdown.Append("string name = \"test\";\n");
            markdown.Append("```\n");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<pre><code>");
            expectedHtml.Append("string name = &quot;test&quot;;\n");
            expectedHtml.Append("</code></pre>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderCodeblocksWithSyntaxName()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("```cs");
            markdown.AppendLine("string name = \"test\";");
            markdown.AppendLine("```");

            var expectedHtml = new StringBuilder();

            // it looks like the syntax name is prefixed with 'lang' by convention
            expectedHtml.Append("<pre><code class=\"lang-cs\">");
            expectedHtml.Append("string name = &quot;test&quot;;\n");
            expectedHtml.Append("</code></pre>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            var document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document);

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
            markdown.AppendLine("   this is item two");
            markdown.AppendLine("3. item");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<ol>");
            expectedHtml.Append("<li><p>item</p></li>");
            expectedHtml.Append("<li><p>item this is item two</p></li>");
            expectedHtml.Append("<li><p>item</p></li>");
            expectedHtml.Append("</ol>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document);

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
            expectedHtml.Append("<li><p>item</p></li>");
            expectedHtml.Append("<li><p>item</p></li>");
            expectedHtml.Append("<li><p>item</p></li>");
            expectedHtml.Append("</ul>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderParagraph()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("some text here");
            markdown.AppendLine("a link here [here](http://www.123.com)");
            markdown.AppendLine("a inline image here ![alt text](/images/sample.jpg)");
            markdown.AppendLine("emphasis of *text* or strong emphasis of **text**");
            markdown.AppendLine("inline code block `var test = 123;` should be supported");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<p>");
            expectedHtml.Append("some text here ");
            expectedHtml.Append("a link here <a href=\"http://www.123.com\">here</a> ");
            expectedHtml.Append("a inline image here <img src=\"/images/sample.jpg\" alt=\"alt text\" /> ");
            expectedHtml.Append("emphasis of <em>text</em> or strong emphasis of <strong>text</strong> ");
            expectedHtml.Append("inline code block <code>var test = 123;</code> should be supported");
            expectedHtml.Append("</p>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void FixParagraphDisappears()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.Append("#### Paragraphs\n");
            markdown.Append("\n");
            markdown.Append("Paragraphs are lines of text followed by empty line.\n");
            markdown.Append("\n");
            markdown.Append("Second paragraph\n");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<h4>Paragraphs</h4>");
            expectedHtml.Append("<p>");
            expectedHtml.Append("Paragraphs are lines of text followed by empty line.");
            expectedHtml.Append("</p>");
            expectedHtml.Append("<p>");
            expectedHtml.Append("Second paragraph");
            expectedHtml.Append("</p>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderParagraphWithOnlyEmphasis()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.Append("*text*");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<p>");
            expectedHtml.Append("<em>text</em>");
            expectedHtml.Append("</p>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderParagraphWithOnlyStrongEmphasis()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.Append("**text**");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<p>");
            expectedHtml.Append("<strong>text</strong>");
            expectedHtml.Append("</p>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void RenderEmpty()
        {
            // arrange
            var markdown = new StringBuilder();
            var expectedHtml = new StringBuilder();
         
            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            Document document = parser.Parse(markdown.ToString());
            string html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        [Fact]
        public void ThrowExceptionOnRenderingError()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("Title");
            markdown.AppendLine("======");
            markdown.AppendLine();

            markdown.Append("1234567890");

            var parser = new MarkdownParser();

            var renderer = new MarkdownHtmlRenderer();
            renderer.Options.Renderers.Insert(
                0,
                new ThrowErrorOnBlockRenderer(typeof(Paragraph)));

            // act
            Document document = parser.Parse(markdown.ToString());
            
            var exception = Assert.Throws<RenderingException>(
                ()=>renderer.Render(document));

            // assert
            exception.Block.Should().BeOfType<Paragraph>();
            exception.Renderer.Should().BeOfType<ThrowErrorOnBlockRenderer>();
            exception.InnerException.Should().BeOfType<ArgumentNullException>();
        }
    }
}