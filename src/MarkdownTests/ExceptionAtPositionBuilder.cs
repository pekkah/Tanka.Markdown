namespace Tanka.MarkdownTests
{
    using System;
    using Markdown;
    using Markdown.Blocks;

    public class ExceptionAtPositionBuilder : IBlockBuilder
    {
        private readonly int _throwAtPosition;

        public ExceptionAtPositionBuilder(int throwAtPosition)
        {
            _throwAtPosition = throwAtPosition;
        }

        public bool CanBuild(int start, StringRange content)
        {
            return start == _throwAtPosition;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            throw new ArgumentNullException();
        }
    }
}