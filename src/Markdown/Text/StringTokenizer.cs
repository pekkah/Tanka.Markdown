namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class StringTokenizer
    {
        public StringTokenizer(IEnumerable<ITokenFactory> factories)
        {
            TokenFactories = new List<ITokenFactory>(factories);
        }

        public StringTokenizer()
        {
            TokenFactories = new List<ITokenFactory>
            {
                new TokenFactory(text => text.StartsWith("["),
                    i => new Token(TokenType.LinkTitleStart, i)),
                new TokenFactory(text => text.StartsWith("]"),
                    i => new Token(TokenType.LinkTitleEnd, i)),
                new TokenFactory(text => text.StartsWith("("),
                    i => new Token(TokenType.LinkUrlStart, i)),
                new TokenFactory(text => text.StartsWith(")"),
                    i => new Token(TokenType.LinkUrlEnd, i)),
                new TokenFactory(text => text.StartsWith("!["),
                    i => new Token(TokenType.Image, i)),
                new TokenFactory(text => true,
                    i => new Token(TokenType.Text, i))
            };
        }

        public List<ITokenFactory> TokenFactories { get; set; }

        public IEnumerable<Token> Tokenize(string content)
        {
            var tokens = new List<Token>();
            Token previousToken = null;

            for (int position = 0; position < content.Length; position++)
            {
                string restOfTheString = content.Substring(position);

                Token currenToken = GetToken(restOfTheString, position);

                // don't create new token if the previous token was text
                if (previousToken != null && previousToken.Type == TokenType.Text)
                {
                    if (previousToken.Type == currenToken.Type)
                        continue;
                }

                tokens.Add(currenToken);

                previousToken = currenToken;
            }

            tokens.Add(new Token(TokenType.End, content.Length));

            return tokens;
        }

        public Token GetToken(string c, int position)
        {
            ITokenFactory factoryEntry = TokenFactories.First(f => f.CanCreate(c));
            return factoryEntry.Create(position);
        }
    }
}