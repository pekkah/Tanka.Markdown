namespace Tanka.Markdown
{
    using System.Text;

    public class StringParser
    {
        private readonly string _text;
        private int _position;
        private readonly int _endPosition;

        public StringParser(string text)
        {
            _text = text;
            _position = 0;
            _endPosition = text.Length-1;
        }

        public char? ReadOne()
        {
            if (_position == _endPosition)
                return null;

            var readChar = _text[_position];

            _position++;
            return (char)readChar;
        }

        public char? PeekOne()
        {
            var nextPosition = _position + 1;

            if (nextPosition > _endPosition)
            {
                return null;
            }

            var readChar = _text[nextPosition];

            return (char)readChar;
        }

        public string ReadUntil(char c)
        {
            var builder = new StringBuilder();

            while (PeekOne() != c)
            {
                char? readChar = ReadOne();

                if (readChar == null)
                {
                    return null;
                }

                builder.Append(readChar);
            }

            return builder.ToString();
        }

        public string ReadUntilEmptyLine()
        {
            return null;
        }
    }
}