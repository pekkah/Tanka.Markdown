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
            Token start = tokens.Pop();
            Token next = tokens.Peek();

            string spanContent = GetTokenContent(
                start.StartPosition,
                next.StartPosition,
                content);

            return new TextSpan
            {
                Content = spanContent
            };
        }
    }

    public class UnknownAsTextSpanFactory : SpanFactoryBase
    {
        public override bool IsMatch(IEnumerable<TokenType> tokens)
        {
            return tokens.Any();
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            Token start = tokens.Pop();
            Token next = tokens.Peek();

            string spanContent = GetTokenContent(
                start.StartPosition,
                next.StartPosition,
                content);

            return new TextSpan
            {
                Content = spanContent
            };
        }
    }
}