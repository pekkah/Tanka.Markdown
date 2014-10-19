namespace Tanka.Markdown
{
    using System;

    public class StringRange : IEquatable<StringRange>
    {
        private readonly string _document;

        public StringRange(string document, int start, int end)
        {
            Start = start;
            End = end;
            _document = document;
        }

        public StringRange(string document) : this(document, 0, document.Length - 1)
        {
        }

        public StringRange(StringRange parentRange, int start, int end)
            : this(parentRange.Document, start, end)
        {
        }

        public int End { get; protected set; }

        public int Start { get; protected set; }

        public int Length
        {
            get { return End - Start + 1; }
        }

        public string Document
        {
            get { return _document; }
        }

        public char this[int position]
        {
            get
            {
                if (position < Start || position > End)
                    throw new ArgumentOutOfRangeException(
                        "position",
                        string.Format("Position {0} must be in range of {1} and {2}", position, Start, End));

                return _document[position];
            }
        }

        public bool Equals(StringRange other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Start == other.Start && End == other.End;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start*397) ^ Length;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((StringRange) obj);
        }

        public int IndexOf(char c, int start)
        {
            if (start < Start)
                throw new ArgumentOutOfRangeException("start");

            if (start > End)
                throw new ArgumentOutOfRangeException("start");

            int length = Length - (start - Start);
            int index = _document.IndexOf(c, start, length);

            return index;
        }

        public int IndexOf(char c)
        {
            return IndexOf(c, Start);
        }

        public int IndexOf(string str, int start)
        {
            if (start < Start)
                throw new ArgumentOutOfRangeException("start");

            if (start > End)
                throw new ArgumentOutOfRangeException("start");

            int length = Length - (start - Start);
            int index = _document.IndexOf(str, start, length, StringComparison.Ordinal);

            return index;
        }

        public int IndexOf(string str)
        {
            return IndexOf(str, Start);
        }

        public int LastIndexOf(char c)
        {
            int index = _document.LastIndexOf(c, Start, Length);

            if (index == -1)
                throw new InvalidOperationException(
                    string.Format("Could not find {0} from {1}", c, _document.Substring(Start, Length)));

            return index;
        }

        public override string ToString()
        {
            string result = _document.Substring(Start, Length);
            return result;
        }

        public bool StartsWith(params char[] characters)
        {
            return HasCharactersAt(Start, characters);
        }

        public int StartOfNextLine(int fromPosition)
        {
            int indexOfNewLine = IndexOf('\n', fromPosition);

            if (indexOfNewLine == -1)
                return indexOfNewLine;

            if (indexOfNewLine == End)
                return -1;

            return indexOfNewLine + 1;
        }

        public int EndOfLine(int fromPosition, bool includeEndOfLineMarkers = false)
        {
            int indexOfN = IndexOf('\n', fromPosition);

            if (includeEndOfLineMarkers && indexOfN > 0)
                return indexOfN;

            if (indexOfN == -1)
                return End;

            if (indexOfN != -1)
            {
                return indexOfN - 1;
            }

            return End;
        }

        public bool HasCharactersAt(int at, params char[] characters)
        {
            if (at < Start)
                return false;

            if (at > End)
                return false;

            if ((at + characters.Length-1) > End)
                return false;

            for (int i = 0; i < characters.Length; i++)
            {
                char expected = characters[i];
                char actual = _document[at + i];

                if (actual != expected)
                    return false;
            }

            return true;
        }

        public bool HasStringAt(int at, string str)
        {
            if (at < Start)
                return false;

            if (at > End)
                return false;

            if ((at + str.Length -1) > End)
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                char expected = str[i];
                char actual = _document[at + i];

                if (actual != expected)
                    return false;
            }

            return true;
        }

        public bool IsStartOfLine(int start)
        {
            if (start == Start)
                return true;

            return HasCharactersAt(start - 1, '\n');
        }
    }
}