namespace Tanka.MarkdownTests
{
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
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
            listBlockBuilder.End(lines[0]);

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
            listBlockBuilder.End(lines[0]);

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
            listBlockBuilder.End(lines[0]);

            var listBlock = listBlockBuilder.Create() as ListBlock;
            listBlock.Items.Should().ContainSingle(item => item == "item");
        } 

    }
}