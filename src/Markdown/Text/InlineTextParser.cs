namespace Tanka.Markdown.Text
{
    using System;
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
                new ReferenceLinkSpanFactory(),
                new TextSpanFactory(),
                new UnknownAsTextSpanFactory()
            };
        }

        public IEnumerable<ISpan> Parse(string content)
        {
            Stack<Token> tokens = GetTokens(content);
            var result = new List<ISpan>();

            ISpan previousSpan = null;

            while (tokens.Any())
            {
                SpanFactoryBase factory = CreateSpan(tokens.Select(t => t.Type));

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