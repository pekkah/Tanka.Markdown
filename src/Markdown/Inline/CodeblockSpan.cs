namespace Tanka.Markdown.Inline
{
    public class CodeblockSpan : Span
    {
        public CodeblockSpan(StringRange parentRange, int start, int end) : base(parentRange, start, end)
        {
        }
    }
}