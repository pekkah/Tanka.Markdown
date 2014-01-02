namespace Tanka.Markdown.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StringTokenizer
    {
        private readonly string _content;

        private readonly Dictionary<Func<string, bool>, Func<int, Token>> _tokenFactory;

        protected StringTokenizer()
        {
            _tokenFactory = new Dictionary<Func<string, bool>, Func<int, Token>>
            {
                {
                    text => text.StartsWith("["), i => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkTitleStart
                    }
                },
                {
                    text => text.StartsWith("]"), i => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkTitleEnd
                    }
                },
                {
                    text => text.StartsWith("("), i => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkUrlStart
                    }
                },
                {
                    text => text.StartsWith(")"), i => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.LinkUrlEnd
                    }
                },
                {
                    text => text.StartsWith("!["), i => new Token
                    {
                        StartPosition = i,
                        Type = TokenType.Image
                    }
                },
                {
                    text => true, i => new Token
                    {
                        Type = TokenType.Text,
                        StartPosition = i
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
                string twoChars;
                if (position < _content.Length-1)
                {
                    twoChars = _content.Substring(position, 2);
                }
                else
                {
                    twoChars = _content.Substring(position, 1);
                }

                Token currenToken = GetToken(twoChars, position);

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

        public Token GetToken(string c, int position)
        {
            KeyValuePair<Func<string, bool>, Func<int, Token>> factoryEntry = _tokenFactory.First(f => f.Key(c));

            return factoryEntry.Value(position);
        }
    }
}