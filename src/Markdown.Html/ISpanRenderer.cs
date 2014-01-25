namespace Tanka.Markdown.Html
{
    using System.Text;
    using Inline;

    public interface ISpanRenderer
    {
        bool CanRender(Span span);
        void Render(Span span, StringBuilder builder);
    }
}