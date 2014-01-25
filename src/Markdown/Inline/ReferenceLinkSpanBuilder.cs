namespace Tanka.Markdown.Inline
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class ReferenceLinkSpanBuilder : SpanBuilder
    {
        private readonly Regex _expression;

        public ReferenceLinkSpanBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .Then("[")
                .Anything()
                .Then("]")
                .Then("[")
                .Anything()
                .Then("]")
                .ToRegex();

        }

        public override bool CanBuild(int position, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, position);

            return isMatch;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            // title between [ and ]
            var title = new StringRange(
                content,
                content.IndexOf('[', position) + 1,
                content.IndexOf(']', position) - 1);

            // key between [ and ] after title
            var keyStart = content.IndexOf('[', title.End + 1) + 1;
            var keyEnd = content.IndexOf(']', keyStart) - 1;

            var key = new StringRange(
                content,
                keyStart,
                keyEnd);

            // update last position
            lastPosition = key.End + 1;
            return new ReferenceLinkSpan(content, position, lastPosition, title, key);
        }
    }
}