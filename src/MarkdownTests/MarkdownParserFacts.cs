namespace Tanka.MarkdownTests
{
    using System.Linq;
    using System.Net.Mime;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using Markdown.Inline;
    using Xunit;

    public class MarkdownParserFacts
    {
        [Fact]
        public void Paragraph()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("paragraph is just text");
            builder.AppendLine("that can continue for");
            builder.AppendLine("multiple lines");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().ContainSingle(block => block.GetType() == typeof (Paragraph));
            var paragraph = result.Blocks.Single() as Paragraph;
            paragraph.Start.ShouldBeEquivalentTo(0);
            paragraph.Length.ShouldBeEquivalentTo(markdown.Length);
            paragraph.Spans.Should().HaveCount(6);
        }

        [Fact]
        public void MultipleParagraphs()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("paragraph is just text");
            builder.AppendLine("that can continue for");
            builder.AppendLine();
            builder.AppendLine("second paragraph");
            builder.AppendLine("multiple lines");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().HaveCount(2);
            result.Blocks.Should().ContainItemsAssignableTo<Paragraph>();
        }

        [Fact]
        public void MultipleParagraphsWithNewLines()
        {
            /* given */
            var builder = new StringBuilder();
            builder.Append(
                "Paragraphs should be separated by one empty line. Paragraph text is part of one \nparagraph as long as there's no empty lines.\n\nThis is a second paragraph which starts from new line and there's one empty line\nabove it.");

            var markdown = builder.ToString();
            var parser = new MarkdownParser();
            
            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().HaveCount(2);
            result.Blocks.Should().ContainItemsAssignableTo<Paragraph>();
        }

        [Fact]
        public void EmptyLine()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("paragraph is just text");
            builder.AppendLine("that can continue for");
            builder.AppendLine();
            builder.AppendLine("second paragraph");
            builder.AppendLine("multiple lines");
            string markdown = builder.ToString();

            var parser = new MarkdownParser(false);

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().HaveCount(3);
            var firstParagraph = result.Blocks.First() as Paragraph;
            firstParagraph.Start.ShouldBeEquivalentTo(0);
            firstParagraph.End.ShouldBeEquivalentTo(44);

            var emptyLine = result.Blocks.ElementAt(1) as EmptyLine;
            emptyLine.Start.ShouldBeEquivalentTo(45);
            emptyLine.Length.ShouldBeEquivalentTo(4);

            var secondParagraph = result.Blocks.Last() as Paragraph;
            secondParagraph.Start.ShouldBeEquivalentTo(emptyLine.End + 1);
            secondParagraph.End.ShouldBeEquivalentTo(markdown.Length - 1);
        }

        [Fact]
        public void Codeblock()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("```");
            builder.AppendLine("public int X = 1;");
            builder.AppendLine("```");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().HaveCount(1);
            result.Blocks.Should().ContainSingle(block => block.GetType() == typeof (Codeblock));
        }

        [Fact]
        public void SetextHeadings()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("Setext 1");
            builder.AppendLine("========");
            builder.AppendLine("Setext 2");
            builder.AppendLine("--------");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            result.Blocks.Should().HaveCount(2);

            var headingOne = result.Blocks.First() as Heading;
            headingOne.Level.ShouldBeEquivalentTo(1);
            headingOne.ToString().ShouldAllBeEquivalentTo("Setext 1");

            var headingTwo = result.Blocks.Last() as Heading;
            headingTwo.Level.ShouldBeEquivalentTo(2);
            headingTwo.ToString().ShouldAllBeEquivalentTo("Setext 2");
        }

        [Fact]
        public void Headings()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("# Heading 1");
            builder.AppendLine("## Heading 2");
            builder.AppendLine("### Heading 3");
            builder.AppendLine("#### Heading 4");
            builder.AppendLine("##### Heading 5");
            builder.AppendLine("###### Heading 6");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            for (int i = 0; i < result.Blocks.Count(); i++)
            {
                string expected = string.Format("Heading {0}", i + 1);
                var heading = result.Blocks.ElementAt(i) as Heading;

                heading.Level.ShouldBeEquivalentTo(i + 1);
                heading.ToString().ShouldAllBeEquivalentTo(expected);
            }
        }

        [Fact]
        public void UnorederedList()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("* item 1");
            builder.AppendLine("* item 2");
            builder.AppendLine("* item 3");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            var list = result.Blocks.Single() as List;
            list.IsOrdered.ShouldBeEquivalentTo(false);
            for (int i = 0; i < list.Items.Count(); i++)
            {
                string expected = string.Format("item {0}", i + 1);
                Item item = list.Items.ElementAt(i);

                item.ToString().ShouldAllBeEquivalentTo(expected);
            }
        }

        [Fact]
        public void OrdederedList()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("1. item 1");
            builder.AppendLine("2. item 2");
            builder.AppendLine("3. item 3");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            var list = result.Blocks.Single() as List;
            list.IsOrdered.ShouldBeEquivalentTo(true);
            for (int i = 0; i < list.Items.Count(); i++)
            {
                string expected = string.Format("item {0}\r\n", i + 1);
                Item item = list.Items.ElementAt(i);

                item.ToString().ShouldAllBeEquivalentTo(expected);
            }
        }

        [Fact]
        public void LinkDefinitions()
        {
            /* given */
            var builder = new StringBuilder();
            builder.AppendLine("[key 1]: url-1");
            builder.AppendLine("[key 2]: url-2");
            builder.AppendLine("[key 3]: url-3");
            string markdown = builder.ToString();

            var parser = new MarkdownParser();

            /* when */
            Document result = parser.Parse(markdown);

            /* then */
            var list = result.Blocks.Single() as LinkDefinitionList;
            for (int i = 0; i < list.Definitions.Count(); i++)
            {
                string expectedKey = string.Format("key {0}", i + 1);
                string expectedUrl = string.Format("url-{0}", i + 1);

                LinkDefinition definition = list.Definitions.ElementAt(i);

                definition.Key.ToString().ShouldBeEquivalentTo(expectedKey);
                definition.Url.ToString().ShouldBeEquivalentTo(expectedUrl);
            }
        }

        [Fact]
        public void ParagraphWithImage()
        {
            /* given */
            var contentBuilder = new StringBuilder();
            contentBuilder.Append("And images ![alt](http://image.jpg)");
            var parser = new MarkdownParser();

            /* when */
            var result = parser.Parse(contentBuilder.ToString());

            /* then */
            result.Blocks.Should().HaveCount(1);

            var paragaph = (Paragraph) result.Blocks.Single();
            paragaph.Spans.Should().HaveCount(2);
            paragaph.Spans.OfType<TextSpan>().Single().ToString().ShouldAllBeEquivalentTo("And images ");
            var image = paragaph.Spans.OfType<ImageSpan>().Single();
            image.Title.ToString().ShouldBeEquivalentTo("alt");
            image.Url.ToString().ShouldBeEquivalentTo("http://image.jpg");
        }
    }
}
