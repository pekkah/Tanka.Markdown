namespace Tanka.Markdown.Html
{
    using System;
    using System.Text;
    using Inline;

    public class SpanRenderer<T> : ISpanRenderer where T : Span
    {
        private readonly Action<T, StringBuilder> _render;

        public SpanRenderer(Action<T, StringBuilder> render)
        {
            if (render == null) throw new ArgumentNullException("render");

            _render = render;
        }

        public bool CanRender(Span span)
        {
            return span is T;
        }

        public void Render(Span span, StringBuilder builder)
        {
            _render((T) span, builder);
        }
    }
}