namespace Markdown.HtmlTests
{
    using System.Text;
    using FluentAssertions;
    using Tanka.Markdown;
    using Tanka.Markdown.Html;
    using Xunit;

    /// <summary>
    /// http://spec.commonmark.org/0.5/#block-quotes
    /// </summary>
    public partial class RenderAsHtmlFeature
    {
        [Fact]
        public void SimpleBlockQuote()
        {
            var markdown = new StringBuilder();
            markdown.AppendLine(">Foo");

            const string expectedHtml = "<blockquote><p>Foo</p></blockquote>";

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document).Replace("\r\n", "");

            html.ShouldBeEquivalentTo(expectedHtml);
        }

        [Fact]
        public void EmptyBlockQuote()
        {
            var markdown = new StringBuilder();
            markdown.AppendLine(">");

            const string expectedHtml = "<blockquote></blockquote>";

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document).Replace("\r\n", "");

            html.ShouldBeEquivalentTo(expectedHtml);
        }

        /// <summary>
        /// Markdown:
        /// 
        /// > # Foo
        /// > bar
        /// > baz
        /// 
        /// Html:
        /// 
        /// <blockquote>
        /// <h1>Foo</h1>
        /// <p>bar
        /// baz</p>
        /// </blockquote>
        /// 
        /// </summary>
        [Fact]
        public void RenderBlockQuote_138()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("> # Foo");
            markdown.AppendLine("> bar");
            markdown.AppendLine("> baz");

            var expectedHtml = new StringBuilder()
                .Append("<blockquote>")
                .Append("<h1>Foo</h1>")
                .AppendLine("<p>bar") //newline char is significant
                .Append("baz</p>")
                .Append("</blockquote>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        /// <summary>
        /// ># Foo
        /// >bar
        /// > baz
        /// 
        /// <blockquote>
        /// <h1>Foo</h1>
        /// <p>bar
        /// baz</p>
        /// </blockquote>
        /// </summary>
        [Fact]
        public void RenderBlockQuote_139()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("># Foo");
            markdown.AppendLine(">bar");
            markdown.AppendLine("> baz");

            var expectedHtml = new StringBuilder()
                .Append("<blockquote>")
                .Append("<h1>Foo</h1>")
                .AppendLine("<p>bar") //newline char is significant
                .Append("baz</p>")
                .Append("</blockquote>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        /// <summary>
        ///    > # Foo
        ///    > bar
        /// > baz
        /// 
        /// <blockquote>
        /// <h1>Foo</h1>
        /// <p>bar
        /// baz</p>
        /// </blockquote>
        /// </summary>
        [Fact]
        public void RenderBlockQuote_140()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("   > # Foo");
            markdown.AppendLine("   > bar");
            markdown.AppendLine("> baz");

            var expectedHtml = new StringBuilder()
                .Append("<blockquote>")
                .Append("<h1>Foo</h1>")
                .AppendLine("<p>bar") //newline char is significant
                .Append("baz</p>")
                .Append("</blockquote>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }

        /// <summary>
        /// Four spaces gives us a code block: 
        /// 
        ///    > # Foo
        ///    > bar
        ///    > baz
        /// 
        /// <pre><code>&gt; # Foo
        /// &gt; bar
        /// &gt; baz
        /// </code></pre>
        /// </summary>
        [Fact]
        public void RenderBlockQuote_141()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("    > # Foo");
            markdown.AppendLine("    > bar");
            markdown.AppendLine("    > baz");

            var expectedHtml = new StringBuilder()
                .AppendLine("<pre><code>&gt; # Foo")
                .AppendLine("&gt; bar") //newline chars are significant
                .AppendLine("&gt; baz")
                .Append("</code></pre>");

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            // act
            var document = parser.Parse(markdown.ToString());
            var html = renderer.Render(document);

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }
    }
}
