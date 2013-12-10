namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;

    public abstract class SpanFactoryBase
    {
        public abstract bool IsMatch(IEnumerable<TokenType> tokens);
        public abstract ISpan Create(Stack<Token> tokens, string content);

        protected string GetTokenContent(int startPosition, int endPosition, string content)
        {
            var length = endPosition - startPosition;
            return content.Substring(startPosition, length);
        }
    }
}