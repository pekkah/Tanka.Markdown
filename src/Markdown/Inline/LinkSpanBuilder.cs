namespace Tanka.Markdown.Inline
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class LinkSpanBuilder : SpanBuilder
    {
        private readonly Regex _expression;

        public LinkSpanBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .Then("[")
                .Anything()
                .Then("]")
                .Then("(")
                .Anything()
                .Then(")")
                .ToRegex();

        }

        public override bool CanBuild(int position, StringRange content)
        {
            var isMatch =  _expression.IsMatch(content.Document, position);

            return isMatch;
        }

        public override Span Build(int position, StringRange content, out int lastPosition)
        {
            // update the end position
            lastPosition = content.IndexOf(')', position);

            var link = new StringRange(content, position, lastPosition);
            
            // title between [ and ]
            var title = new StringRange(
                content,
                link.IndexOf('[') + 1,
                link.IndexOf(']') - 1);

            // url between ( and )
            var url = new StringRange(
                content,
                link.IndexOf('(') + 1,
                link.IndexOf(')') - 1);

            return new LinkSpan(content, position, lastPosition, title, url);    
        }
    }
}