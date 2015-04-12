namespace Tanka.Markdown.Html
{
    using System;
    using Blocks;

    public class RenderingException : Exception
    {
        public RenderingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public Block Block { get; set; }

        public IBlockRenderer Renderer { get; set; }
    }
}