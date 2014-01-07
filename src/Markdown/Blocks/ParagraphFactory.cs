namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Text;

    public class ParagraphFactory : BlockFactoryBase
    {
        private readonly List<SpanFactoryBase> _factories;

        public ParagraphFactory()
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

            Tokenizer = new StringTokenizer();
        }

        public ParagraphFactory(IEnumerable<SpanFactoryBase> factories, StringTokenizer tokenizer)
        {
            _factories = new List<SpanFactoryBase>(factories);
            Tokenizer = tokenizer;
        }

        public List<SpanFactoryBase> SpanFactories
        {
            get { return _factories; }
        }

        public StringTokenizer Tokenizer { get; set; }

        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(currentLine))
                return false;

            return true;
        }

        public override BlockBuilderBase Create()
        {
            return new ParagraphBuilder(_factories, Tokenizer);
        }
    }
}