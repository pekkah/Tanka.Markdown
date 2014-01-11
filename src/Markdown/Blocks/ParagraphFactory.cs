namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Text;

    public class ParagraphFactory : BlockFactoryBase
    {
        private readonly List<SpanFactoryBase> _factories;

        public ParagraphFactory(IEnumerable<SpanFactoryBase> factories)
        {
            Tokenizer = new StringTokenizer();
            _factories = new List<SpanFactoryBase>(factories);
        }

        public ParagraphFactory(IEnumerable<SpanFactoryBase> factories, StringTokenizer tokenizer)
        {
            _factories = new List<SpanFactoryBase>(factories);
            Tokenizer = tokenizer;
        }

        public ParagraphFactory()
        {
            _factories = new List<SpanFactoryBase>();
            Tokenizer = new StringTokenizer();
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