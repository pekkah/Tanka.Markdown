namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class EndFactory : SpanFactoryBase
    {
        public override bool IsMatch(IEnumerable<Token> tokens)
        {
            return tokens.First().Type == TokenType.End;
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            tokens.Pop();
            return new EmptySpan();
        }
    }
}