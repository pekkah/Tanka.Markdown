namespace Tanka.Markdown.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinkSpan : ISpan
    {
        public string Title { get; set; }

        public string UrlOrKey { get; set; }

        public bool IsKey { get; set; }
    }

    public class LinkSpanFactory : SpanFactoryBase
    {
        private static readonly List<TokenType> Pattern = new List<TokenType>
        {
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
            // tokens: [text](text)

            // pop the link title start token [
            tokens.Pop();

            // title content between these
            var titleStartToken = tokens.Pop();
            var titleEndToken = tokens.Pop();

            // pop the link url or key start token (
            tokens.Pop();

            // url content between these
            var urlStartToken = tokens.Pop();
            var urlEndToken = tokens.Pop();

            // get title content
            var title = GetTokenContent(
                titleStartToken.StartPosition,
                titleEndToken.StartPosition,
                content);

            var urlOrKey = GetTokenContent(
                urlStartToken.StartPosition,
                urlEndToken.StartPosition,
                content);

            return new LinkSpan()
            {
                Title = title,
                UrlOrKey = urlOrKey
            };
        }
    }
}