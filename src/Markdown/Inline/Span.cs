namespace Tanka.Markdown.Inline
{
    public abstract class Span : StringRange
    {
        public bool CleanInput { get; set; }

        protected Span(StringRange parentRange, int start, int end, bool cleanInput =true)
            : base(parentRange.Document, start, end)
        {
            CleanInput = cleanInput;
        }
    }
}