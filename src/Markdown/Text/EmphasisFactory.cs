namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class EmphasisFactory : SpanFactoryBase
    {
        public override bool IsMatch(IEnumerable<Token> tokens)
        {
            Token first = tokens.First();

            if (first.Type == TokenType.Emphasis || first.Type == TokenType.StrongEmphasis)
                return true;

            return false;
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            // first *
            Token first = tokens.Pop();
            
            bool isStrong = first.Type == TokenType.StrongEmphasis;

            if (isStrong)
            {
                // second *
                tokens.Pop();
                return new StrongEmphasisBeginOrEnd();
            }

            return new EmphasisBeginOrEnd();
        }
    }
}