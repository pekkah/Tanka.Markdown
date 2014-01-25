namespace Tanka.Markdown.Inline
{
    public class CharSpanBuilder : SpanBuilder
    {
        public override bool CanBuild(int position, StringRange content)
        {
            return true;
        }

        public override Span Build(int position, StringRange content, out int newPosition)
        {
            newPosition = position + 1;
            return null;
        }
    }
}