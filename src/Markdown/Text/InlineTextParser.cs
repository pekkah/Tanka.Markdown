namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class InlineTextParser
    {
        private readonly List<SpanFactoryBase> _factories;

        public InlineTextParser()
        {
            _factories = new List<SpanFactoryBase>
            {
                new EndFactory(),
                new ImageSpanFactory(),
                new LinkSpanFactory(),
                new TextSpanFactory(),
            };
        }

        public IEnumerable<ISpan> Parse(string content)
        {
            Stack<Token> tokens = GetTokens(content);

            while (tokens.Any())
            {
                SpanFactoryBase factory = CreateSpan(tokens.Select(t => t.Type));

                var span = factory.Create(tokens, content);

                if (span is EmptySpan)
                    continue;

                yield return span;
            }
        }

        private SpanFactoryBase CreateSpan(IEnumerable<TokenType> tokens)
        {
            return _factories.First(f => f.IsMatch(tokens));
        }

        private Stack<Token> GetTokens(string content)
        {
            var tokenizer = new StringTokenizer(content);
            IEnumerable<Token> tokens = tokenizer.Tokenize();

            return new Stack<Token>(tokens.Reverse());
        }
    }

    public interface ISpan
    {
    }
}