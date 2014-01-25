namespace Tanka.Markdown.Inline
{
    public abstract class SpanBuilder
    {
        public abstract bool CanBuild(int position, StringRange content);

        public abstract Span Build(int position, StringRange content, out int lastPosition);
    }
}