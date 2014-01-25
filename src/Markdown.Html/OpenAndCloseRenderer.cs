namespace Tanka.Markdown.Html
{
    using System;
    using System.Text;
    using Inline;

    public class OpenAndCloseRenderer<T> : ISpanRenderer where T : Span
    {
        private readonly string _tag;
        private bool _hasStarted;

        public OpenAndCloseRenderer(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentException("tag");

            _tag = tag;
        }

        public bool CanRender(Span span)
        {
            if (span == null) throw new ArgumentNullException("span");

            return span is T;
        }

        public void Render(Span span, StringBuilder builder)
        {
            if (span == null) throw new ArgumentNullException("span");
            if (builder == null) throw new ArgumentNullException("builder");

            Render((T) span, builder);
        }

        protected virtual void Render(T span, StringBuilder builder)
        {
            if (_hasStarted)
            {
                builder.AppendFormat("</{0}>", _tag);
                return;
            }

            _hasStarted = true;
            builder.AppendFormat("<{0}>", _tag);
        }
    }
}