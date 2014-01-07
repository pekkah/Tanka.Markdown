namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class ImageSpan : ISpan
    {
        public string AltText { get; set; }

        public string UrlOrKey { get; set; }

        public bool IsKey { get; set; }
    }

    public class ImageSpanFactory : SpanFactoryBase
    {
        private static readonly List<Token> Pattern = new List<Token>
        {
            new Token(TokenType.Image),
            new Token(TokenType.LinkTitleStart),
            new Token(TokenType.Text),
            new Token(TokenType.LinkTitleEnd),
            new Token(TokenType.LinkUrlStart),
            new Token(TokenType.Text),
            new Token(TokenType.LinkUrlEnd)
        };

        public override bool IsMatch(IEnumerable<Token> tokens)
        {
            return TokensMatch(tokens, Pattern);
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            // tokens: ![text](text)

            // pop the image start token !
            tokens.Pop();

            // pop the link title start token [
            tokens.Pop();

            // title content between these
            Token titleStartToken = tokens.Pop();
            Token titleEndToken = tokens.Pop();

            // pop the link url or key start token (
            tokens.Pop();

            // url content between these
            Token urlStartToken = tokens.Pop();
            Token urlEndToken = tokens.Pop();

            // get title content
            string title = GetTokenContent(
                titleStartToken.StartPosition,
                titleEndToken.StartPosition,
                content);

            string urlOrKey = GetTokenContent(
                urlStartToken.StartPosition,
                urlEndToken.StartPosition,
                content);

            return new ImageSpan()
            {
                AltText = title,
                UrlOrKey = urlOrKey
            };
        }
    }
}