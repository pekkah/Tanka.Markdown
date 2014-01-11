namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public class TextSpanParser
    {
        private readonly List<SpanFactoryBase> _factories;
        private readonly StringTokenizer _tokenizer;

        public TextSpanParser(IEnumerable<SpanFactoryBase> factories, StringTokenizer tokenizer)
        {
            _factories = new List<SpanFactoryBase>(factories);
            _tokenizer = tokenizer;

            EnsureEndFactoryPresent();
        }

        private void EnsureEndFactoryPresent()
        {
            if (_factories.Any(factory => factory is EndFactory))
                return;

            _factories.Add(new EndFactory());
        }

        public IEnumerable<ISpan> Parse(string content)
        {
            Stack<Token> tokens = GetTokens(content);
            var result = new List<ISpan>();

            ISpan previousSpan = null;

            while (tokens.Any())
            {
                SpanFactoryBase factory = GetFactory(tokens);

                ISpan span = factory.Create(tokens, content);

                if (span is EmptySpan)
                    continue;

                // combine text spans
                if (previousSpan is TextSpan && span is TextSpan)
                {
                    var previousTextSpan = previousSpan as TextSpan;
                    var currentTextSpan = span as TextSpan;
                    previousTextSpan.Content = string.Concat(previousTextSpan.Content, "", currentTextSpan.Content);

                    continue;
                }

                previousSpan = span;
                result.Add(span);
            }

            return result;
        }

        private SpanFactoryBase GetFactory(IEnumerable<Token> tokens)
        {
            return _factories.First(f => f.IsMatch(tokens));
        }

        private Stack<Token> GetTokens(string content)
        {
            IEnumerable<Token> tokens = _tokenizer.Tokenize(content);

            return new Stack<Token>(tokens.Reverse());
        }
    }
}