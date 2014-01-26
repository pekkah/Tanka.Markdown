namespace Tanka.Markdown.Inline
{
    using System.Collections.Generic;
    using System.Linq;

    public class InlineParser
    {
        private readonly List<SpanBuilder> _spanBuilders;

        public InlineParser()
        {
            _spanBuilders = new List<SpanBuilder>
            {
                new LinkSpanBuilder(),
                new ImageSpanBuilder(),
                new ReferenceLinkSpanBuilder(),
                new EmphasisBuilder(),
                new CharSpanBuilder()
            };
        }

        public List<SpanBuilder> Builders
        {
            get { return _spanBuilders; }
        }

        public IEnumerable<Span> Parse(StringRange content)
        {
            bool textSpanStarted = false;
            int textSpanStart = -1;
            int textSpanEnd = -1;

            for (int position = content.Start; position <= content.End; position++)
            {
                // get span builder
                SpanBuilder builder = GetBuilder(position, content);

                /*******************************************************
                 * We don't want to generate char spans with only single
                 * character so we combine it to one text span. 
                 * 
                 * CharSpanBuilder cannot actually build anything so it
                 * acts only as marker :)
                 * *****************************************************/
                if (builder is CharSpanBuilder)
                {
                    // text span not started
                    if (!textSpanStarted)
                    {
                        // start from current position
                        textSpanStart = position;
                        textSpanStarted = true;
                    }

                    // just update the end position
                    textSpanEnd = position;
                    continue;
                }

                if (textSpanStarted)
                {
                    // so new span is starting and we have unfinished text span
                    // which we need to yield first
                    yield return new TextSpan(content, textSpanStart, textSpanEnd);
                    textSpanStarted = false;
                }

                // build span and move position to new position
                yield return builder.Build(position, content, out position);
            }

            // so the only or the last span was the text span?
            // lets yield it so it's not left orphaned
            if (textSpanStarted)
            {
                bool isNewLine = content.HasCharactersAt(textSpanStart, '\r', '\n');

                if (isNewLine)
                    yield break;

                yield return new TextSpan(content, textSpanStart, textSpanEnd);
            }
        }

        private SpanBuilder GetBuilder(int position, StringRange content)
        {
            SpanBuilder builder = _spanBuilders.First(b => b.CanBuild(position, content));

            return builder;
        }
    }
}