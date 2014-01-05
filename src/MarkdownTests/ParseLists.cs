namespace Tanka.MarkdownTests
{
    using System.Text;
    using Markdown.Blocks;
    using Xbehave;

    public class ParseLists : MarkdownParserFactsBase
    {
        [Scenario]
        public void ListsWithItemsStartingWithStar()
        {
            var builder = new StringBuilder();
            builder.AppendLine("* item 1");
            builder.AppendLine("* item 2");
            builder.AppendLine("* item 3");

            "Given list in markdown"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be a list with 3 items"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3,
                    Style = ListStyle.Unordered
                }, l => l.Items));
        }

        [Scenario]
        public void ListsWithItemsStartingWithHyphen()
        {
            var builder = new StringBuilder();
            builder.AppendLine("- item 1");
            builder.AppendLine("- item 2");
            builder.AppendLine("- item 3");

            "Given list in markdown"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be a list with 3 items"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3,
                    Style = ListStyle.Unordered
                }, l => l.Items));
        }

        [Scenario]
        public void ListsWithItemsStartingWithNumber()
        {
            var builder = new StringBuilder();
            builder.AppendLine("1. item 1");
            builder.AppendLine("2. item 2");
            builder.AppendLine("3. item 3");

            "Given list in markdown"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be a list with 3 items"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<ListBlock>(0, new
                {
                    Count = 3,
                    Style = ListStyle.Ordered
                }, l => l.Items));
        }

        [Scenario]
        public void ListInsideOtherContent()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Something here");
            builder.AppendLine();
            builder.AppendLine("1. item 1");
            builder.AppendLine("2. item 2");
            builder.AppendLine("3. item 3");
            builder.AppendLine();
            builder.AppendLine("footer content here");

            "Given list in markdown"
                .Given(() =>
                {
                    GivenMarkdownParserWithDefaults();
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(3));

            "And child at index 0 should be a list with 3 items"
                .Then(() => ThenDocumentChildAtIndexShouldMatch<ListBlock>(1, new
                {
                    Count = 3,
                    Style = ListStyle.Ordered
                }, l => l.Items));
        }
    }
}