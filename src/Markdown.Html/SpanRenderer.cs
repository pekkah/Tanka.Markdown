namespace Tanka.Markdown.Html
{
    using System;
    using System.Text;
    using Text;

    public class SpanRenderer<T> : ISpanRenderer where T : ISpan
    {
        private readonly Action<T, StringBuilder> _render;

        public SpanRenderer(Action<T, StringBuilder> render)
        {
            if (render == null) throw new ArgumentNullException("render");

            _render = render;
        }

        public bool CanRender(ISpan span)
        {
            return span is T;
        }

        public void Render(ISpan span, StringBuilder builder)
        {
            _render((T) span, builder);
        }
    }
}