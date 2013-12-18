namespace Tanka.Markdown.Html
{
    using System;
    using Blocks;
    using HtmlTags;

    public class HeadingRenderer : IBlockRenderer
    {
        public bool CanRender(Block block)
        {
            if (block == null) throw new ArgumentNullException("block");
            return block is Heading;
        }

        public HtmlTag Render(Block block)
        {
            if (block == null) throw new ArgumentNullException("block");
            if (!CanRender(block))
                throw new InvalidOperationException(
                    string.Format("Cannot render block of type {0}", block.GetType().FullName));

            var heading = block as Heading;

            var tag = new HtmlTag(String.Format("h{0}", heading.Level));
            tag.Text(heading.Text);

            return tag;
        }
    }
}