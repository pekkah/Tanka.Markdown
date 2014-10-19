namespace Tanka.MarkdownTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using Markdown.Inline;
    using Xbehave;

    public class ExtendByAddingNewSpan : MarkdownParserFactsBase
    {
        [Scenario]
        public void TwitterUserNameSpan()
        {
            var builder = new StringBuilder();
            builder.AppendLine("some random text here and a twitter user name @pekkath plus more text");

            "Given markdown parser with UserNameSpanFactory"
                .Given(() =>
                {
                    var parser = new MarkdownParser();
                    var paragraphBuilder = parser.Builders.OfType<ParagraphBuilder>().Single();
                    paragraphBuilder.InlineParser.Builders.Insert(0, new UserNameBuilder());

                    GivenMarkdownParser(parser);
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(()=>WhenTheMarkdownIsParsed());

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be a paragraph with UserName span"
                .And(() => ThenDocumentChildAtIndexShould<Paragraph>(0, paragraph =>
                {
                    var userName = paragraph.Spans.OfType<UserName>().Single();
                    userName.ToString().ShouldBeEquivalentTo("@pekkath");
                }));
        }
    }

    public class UserNameBuilder : SpanBuilder
    {
        public override bool CanBuild(int position, StringRange content)
        {
            var atPosition = content.IndexOf('@', position);

            if (atPosition == -1)
                return false;

            return true;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            var atPosition = content.IndexOf('@', position);

            lastPosition = content.IndexOf(' ', atPosition)- 1;

            return new UserName(content, atPosition, lastPosition);
        }
    }

    public class UserName : Span
    {
        public UserName(StringRange parentRange, int start, int end) : base(parentRange, start, end)
        {
        }
    }
}