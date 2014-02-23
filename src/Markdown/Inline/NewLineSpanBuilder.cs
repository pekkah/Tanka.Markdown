namespace Tanka.Markdown.Inline
{
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class NewLineSpanBuilder : SpanBuilder
    {
        private readonly Regex _expression;

        public NewLineSpanBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .LineBreak()
                .ToRegex();
        }

        public override bool CanBuild(int position, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, position);

            return isMatch;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            lastPosition = content.EndOfLine(position, true);

            return new NewLineSpan(content, position, lastPosition);
        }
    }

    public class NewLineSpan : Span
    {
        public NewLineSpan(StringRange parentRange, int start, int end) : base(parentRange, start, end)
        {
        }
    }
}