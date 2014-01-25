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
            markdown.AppendLine("1. item");
            markdown.AppendLine("2. item");
            markdown.AppendLine("   this is item two");
            markdown.AppendLine("3. item");
            var listRange = new StringRange(markdown.ToString());

            var builder = new OrderedListBuilder();

            /* when */
            
            var list = (List)builder.Build(0, listRange, out listEnd);

            /* then */
            list.Items.Should().HaveCount(3);

            var item1 = list.Items.ElementAt(0);
            item1.ToString().ShouldBeEquivalentTo("item\r\n");

            var item2 = list.Items.ElementAt(1);
            item2.ToString().ShouldBeEquivalentTo("item\r\n   this is item two\r\n");

            var item3 = list.Items.ElementAt(2);
            item3.ToString().ShouldBeEquivalentTo("item\r\n");
        }
    }
}