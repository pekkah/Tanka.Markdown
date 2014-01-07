namespace Tanka.Markdown.Text
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class SpanFactoryBase
    {
        public abstract bool IsMatch(IEnumerable<Token> tokens);
        public abstract ISpan Create(Stack<Token> tokens, string content);

        protected string GetTokenContent(int startPosition, int endPosition, string content)
        {
            int length = endPosition - startPosition;
            return content.Substring(startPosition, length);
        }

        protected static bool TokensMatch(IEnumerable<Token> wholeSeries, IEnumerable<Token> partOfTheSeries)
        {
            var series = new List<Token>(wholeSeries);
            var part = new List<Token>(partOfTheSeries);

            if (series.Count < part.Count)
                return false;

            for (int i = 0; i < part.Count; i++)
            {
                var partToken = part[i];
                var seriesToken = series[i];

                if (partToken.Type != seriesToken.Type)
                    return false;
            }

            return true;
        }
    }
}