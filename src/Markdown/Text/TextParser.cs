namespace Tanka.Markdown.Text
{
    using System;
    using System.Collections.Generic;

    public class StringTokenizer
    {
        private readonly string _content;

        private readonly Dictionary<char, Func<char, int, Token>> _tokenFactory;

        protected StringTokenizer()
        {
            _tokenFactory = new Dictionary<char, Func<char, int, Token>>
            {
                {
                    '[', (c, i) => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkTitleStart
                    }
                },
                {
                    ']', (c, i) => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkTitleEnd
                    }
                },
                {
                    '(', (c, i) => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkUrlStart
                    }
                },
                {
                    ')', (c, i) => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkUrlEnd
                    }
                }
            };
        }

        public StringTokenizer(string content) : this()
        {
            _content = content;
        }

        public IEnumerable<Token> Tokenize()
        {
            var tokens = new List<Token>();
            Token previousToken = null;

            for (int position = 0; position < _content.Length; position++)
            {
                char c = _content[position];

                Token currenToken = GetToken(c, position);

                // don't create new token if the previous token was text
                if (previousToken != null && previousToken.Type == TokenType.Text)
                {
                    if (previousToken.Type == currenToken.Type)
                        continue;
                }

                tokens.Add(currenToken);

                previousToken = currenToken;
            }

            tokens.Add(new Token {StartPosition = _content.Length, Type = TokenType.End});

            return tokens;
        }

        public Token GetToken(char c, int position)
        {
            Func<char, int, Token> factory = null;
            if (_tokenFactory.TryGetValue(c, out factory))
            {
                return factory(c, position);
            }

            // if not special token then must be text
            return new Token
            {
                Type = TokenType.Text,
                StartPosition = position
            };
        }
    }
}