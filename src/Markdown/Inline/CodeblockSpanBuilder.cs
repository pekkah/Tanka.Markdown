namespace Tanka.Markdown.Inline
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class CodeblockSpanBuilder : SpanBuilder
    {
        private readonly Regex _expression;

        public CodeblockSpanBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .Then("`")
                .Anything()
                .Then("`")
                .ToRegex();
        }

        public override bool CanBuild(int position, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, position);

            return isMatch;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            lastPosition = content.IndexOf('`', position + 1);
            return new CodeblockSpan(content, position + 1, lastPosition - 1);
        }
    }
}