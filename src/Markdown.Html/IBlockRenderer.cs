namespace Tanka.Markdown.Html
{
    using System;
    using HtmlTags;

    public interface IBlockRenderer
    {
        bool CanRender(Block block);
        HtmlTag Render(Document document, Block block);
    }

    public abstract class BlockRendererBase<T> : IBlockRenderer where T : Block
    {
        public bool CanRender(Block block)
        {
            if (block == null) throw new ArgumentNullException("block");

            return CanRender(block as T);
        }

        public HtmlTag Render(Document document, Block block)
        {
            if (block == null) throw new ArgumentNullException("block");

            return Render(document, (T) block);
        }

        protected virtual bool CanRender(T block)
        {
            return block != null;
        }

        protected abstract HtmlTag Render(Document document, T block);
    }
}