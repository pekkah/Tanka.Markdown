namespace Markdown.HtmlTests
{
    using System;
    using HtmlTags;
    using Tanka.Markdown;
    using Tanka.Markdown.Blocks;
    using Tanka.Markdown.Html;

    public class ThrowErrorOnBlockRenderer : IBlockRenderer
    {
        private readonly Type _throwOnBlockType;

        public ThrowErrorOnBlockRenderer(Type throwOnBlockType)
        {
            _throwOnBlockType = throwOnBlockType;
        }

        public bool CanRender(Block block)
        {
            return block.GetType() == _throwOnBlockType;
        }

        public HtmlTag Render(Document document, Block block)
        {
            throw new ArgumentNullException();
        }
    }
}