namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class InlineTextParser
    {
        private readonly List<SpanFactoryBase> _factories; 

        public InlineTextParser()
        {
            _factories = new List<SpanFactoryBase>()
            {
                new TextSpanFactory()
            };
        }

        public IEnumerable<ISpan> Parse(string content)
        {
            var tokens = GetTokens(content);

            while (tokens.Any())
            {
                var factory = CreateSpan(tokens.Select(t => t.Type));

                yield return factory.Create(tokens, content);
            }
        }

        private SpanFactoryBase CreateSpan(IEnumerable<TokenType> tokens)
        {
            return _factories.First(f => f.IsMatch(tokens));
        }

        private Stack<Token> GetTokens(string content)
        {
            var tokenizer = new StringTokenizer(content);
            var tokens = tokenizer.Tokenize();

            return new Stack<Token>(tokens.Reverse());
        }
    }

    public interface ISpan
    {
        
    }
}