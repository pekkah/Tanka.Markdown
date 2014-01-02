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
        private static readonly List<TokenType> Pattern = new List<TokenType>
        {
            TokenType.Image,
            TokenType.LinkTitleStart,
            TokenType.Text,
            TokenType.LinkTitleEnd,
            TokenType.LinkUrlStart,
            TokenType.Text,
            TokenType.LinkUrlEnd
        };

        public override bool IsMatch(IEnumerable<TokenType> tokens)
        {
            IList<TokenType> tokenTypes = tokens as IList<TokenType> ?? tokens.ToList();
            if (tokenTypes.Count() < Pattern.Count)
                return false;

            for (int i = 0; i < Pattern.Count; i++)
            {
                TokenType pt = Pattern[i];
                TokenType st = tokenTypes.ElementAt(i);

                if (pt != st)
                    return false;
            }

            return true;
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