namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class TextSpan : ISpan
    {
        public string Content { get; set; }
    }

    public class TextSpanFactory : SpanFactoryBase
    {
        public override bool IsMatch(IEnumerable<TokenType> tokens)
        {
            return tokens.First() == TokenType.Text;
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            var start = tokens.Pop();
            var next = tokens.Pop();

            var spanContent = GetTokenContent(
                start.StartPosition, 
                next.StartPosition, 
                content);

            return new TextSpan()
            {
                Content = spanContent
            };
        }
    }
}