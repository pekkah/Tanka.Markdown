namespace Tanka.Markdown.Text
{
    using System;

    public class Token : IEquatable<Token>
    {
        public int StartPosition;
        public readonly string Type;

        public Token(string type, int startPosition)
        {
            Type = type;
            StartPosition = startPosition;
        }

        public Token(string type)
        {
            Type = type;
            StartPosition = -1;
        }

        public Token()
        {
            Type = TokenType.Unknown;
            StartPosition = -1;
        }

        public bool Equals(Token other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return StartPosition == other.StartPosition && string.Equals(Type, other.Type);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StartPosition*397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Token) obj);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Type, StartPosition);
        }
    }
}