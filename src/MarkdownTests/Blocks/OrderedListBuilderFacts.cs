namespace Tanka.MarkdownTests.Blocks
{
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using Xunit;

    public class OrderedListBuilderFacts
    {
        [Fact]
        public void Build()
        {
            /* given */
            int listEnd;
            var markdown = new StringBuilder();
            markdown.Append("1. item 1\n");
            markdown.Append("2. item\n");
            markdown.Append("   this is item two 2\n");
            markdown.Append("3. item 3\n");
            var listRange = new StringRange(markdown.ToString());

            var builder = new OrderedListBuilder();

            /* when */
            
            var list = (List)builder.Build(0, listRange, out listEnd);

            /* then */
            list.Items.Should().HaveCount(3);

            var item1 = list.Items.ElementAt(0);
            item1.ToString().ShouldBeEquivalentTo("item 1");

            var item2 = list.Items.ElementAt(1);
            item2.ToString().ShouldBeEquivalentTo("item\n   this is item two 2");

            var item3 = list.Items.ElementAt(2);
            item3.ToString().ShouldBeEquivalentTo("item 3");
        }

        [Fact]
        public void MultilineItems()
        {
            /* given */
            int listEnd;
            var markdown = new StringBuilder();
            markdown.Append("1. item\n");
            markdown.Append("   something here for the next line of the item\n");
            markdown.Append("2. item\n");
            markdown.Append("   this is item two\n");
            markdown.Append("3. item\n");
            markdown.Append("should not the part of item 3\n");
            var listRange = new StringRange(markdown.ToString());

            var builder = new OrderedListBuilder();

            /* when */

            var list = (List)builder.Build(0, listRange, out listEnd);

            /* then */
            list.Items.Should().HaveCount(3);

            var item1 = list.Items.ElementAt(0);
            item1.ToString().ShouldBeEquivalentTo("item\n   something here for the next line of the item");

            var item2 = list.Items.ElementAt(1);
            item2.ToString().ShouldBeEquivalentTo("item\n   this is item two");

            var item3 = list.Items.ElementAt(2);
            item3.ToString().ShouldBeEquivalentTo("item");
        }
    }
}