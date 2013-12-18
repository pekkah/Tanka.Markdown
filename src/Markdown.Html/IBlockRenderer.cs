namespace Tanka.Markdown.Html
{
    using HtmlTags;

    public interface IBlockRenderer
    {
        bool CanRender(Block block);
        HtmlTag Render(Block block);
    }
}