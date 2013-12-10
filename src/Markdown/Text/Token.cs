namespace Tanka.Markdown.Text
{
    public class Token
    {
        public TokenType Type { get; set; }
        public int StartPosition { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type*397) ^ StartPosition;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Token) obj);
        }

        protected bool Equals(Token token)
        {
            return Type == token.Type && StartPosition == token.StartPosition;
        }
    }
}