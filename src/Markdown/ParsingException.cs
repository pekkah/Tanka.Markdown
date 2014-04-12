namespace Tanka.Markdown
{
    using System;

    public class ParsingException : Exception
    {
        public ParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public int Position { get; set; }

        public Type BuilderType { get; set; }

        public StringRange Content { get; set; }
    }
}