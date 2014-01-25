namespace Tanka.Markdown.Inline
{
    public class EmphasisBuilder : SpanBuilder
    {
        public override bool CanBuild(int position, StringRange content)
        {
            bool starts = content[position] == '*';

            if (position >= content.End)
                return starts;

            if (content[position + 1] == '.')
                return false;

            return starts;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            lastPosition = position;

            if (position != content.Length - 1)
            {
                var next = content[position + 1];

                // strong emphasis **
                if (next == '*')
                {
                    lastPosition++;
                    return new StrongEmphasis(content, position);
                }
            }

            return new Emphasis(content, position);
        }
    }
}