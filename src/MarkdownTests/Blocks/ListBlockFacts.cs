namespace Tanka.MarkdownTests.Blocks
{
    using FluentAssertions;
    using Markdown.Blocks;
    using Xunit;

    public class ListBlockFacts
    {
        [Fact]
        public void StripStar()
        {
            var lines = new[]
            {
                "* item"
            };

            var listBlockBuilder = new ListBlockBuilder(ListStyle.Unordered);
            listBlockBuilder.AddLine(lines[0]);
            listBlockBuilder.End();

            var listBlock = listBlockBuilder.Create() as ListBlock;
            listBlock.Items.Should().ContainSingle(item => item == "item");
        }


        [Fact]
        public void StripHyphen()
        {
            var lines = new[]
            {
                "- item"
            };

            var listBlockBuilder = new ListBlockBuilder(ListStyle.Unordered);
            listBlockBuilder.AddLine(lines[0]);
            listBlockBuilder.End();

            var listBlock = listBlockBuilder.Create() as ListBlock;
            listBlock.Items.Should().ContainSingle(item => item == "item");
        }


        [Fact]
        public void StripNumber()
        {
            var lines = new[]
            {
                "1. item"
            };

            var listBlockBuilder = new ListBlockBuilder(ListStyle.Ordered);
            listBlockBuilder.AddLine(lines[0]);
            listBlockBuilder.End();

            var listBlock = listBlockBuilder.Create() as ListBlock;
            listBlock.Items.Should().ContainSingle(item => item == "item");
        }

        [Fact]
        public void IndentedNextLineShouldBeAddedToCurrentItem()
        {
            var builder = new ListBlockBuilder(ListStyle.Ordered);

            // add the item
            builder.AddLine("1. item one");

            // add second line that should be part of first item in list
            builder.AddLine("   second line of item one");

            // end the list
            builder.End();

            // assert
            var list = builder.Create() as ListBlock;
            list.Items.Should().HaveCount(1);
        }

        [Fact]
        public void EmptyLineIsEndLine()
        {
            var builder = new ListBlockBuilder(ListStyle.Ordered);

            builder.AddLine("1. item one");
            builder.IsEndLine("", "").ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void EmptyLineAndNonIndentedNextLineIsEndLine()
        {
            var builder = new ListBlockBuilder(ListStyle.Ordered);

            builder.AddLine("1. item one");
            builder.IsEndLine("", "something not part of the list").ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void EmptyLineAndIndentedNextLineIsNotEndLine()
        {
            var builder = new ListBlockBuilder(ListStyle.Ordered);

            builder.AddLine("1. item one");
            builder.IsEndLine("", "   item one continues").ShouldBeEquivalentTo(false);
        }
    }
}