namespace Tanka.Markdown.Html
{
    using System.Text;
    using Text;

    public interface ISpanRenderer
    {
        bool CanRender(ISpan span);
        void Render(ISpan span, StringBuilder builder);
    }
}