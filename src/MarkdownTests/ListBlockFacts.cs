namespace Tanka.MarkdownTests
{
    using FluentAssertions;
    using Markdown;
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

            var listBlockBuilder = new ListBlockBuilder();
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

            var listBlockBuilder = new ListBlockBuilder();
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

            var listBlockBuilder = new ListBlockBuilder();
            listBlockBuilder.AddLine(lines[0]);
            listBlockBuilder.End();

            var listBlock = listBlockBuilder.Create() as ListBlock;
            listBlock.Items.Should().ContainSingle(item => item == "item");
        }
    }
}