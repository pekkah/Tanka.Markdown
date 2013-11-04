namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class LineReader
    {
        private int _currentLineNumber;
        private IList<string> _lines;

        public LineReader(string document)
        {
            if (document == null) throw new ArgumentNullException("document");

            LoadLines(document);
            _currentLineNumber = 0;
        }

        public int CurrentLineNumber
        {
            get { return _currentLineNumber; }
        }

        private void LoadLines(string document)
        {
            if (_lines == null)
                _lines = new List<string>();

            var reader = new StringReader(document);
            var line = (string) null;

            while ((line = reader.ReadLine()) != null)
                _lines.Add(line);
        }

        public string ReadLine()
        {
            if (EndOfDocument)
                return null;

            var currentLine = _lines[_currentLineNumber];

            _currentLineNumber++;
            return currentLine;
        }

        public IEnumerable<string> ReadLines(int count)
        {
            for (int i = 0; i < count; i++)
                yield return ReadLine();
        }

        public bool EndOfDocument
        {
            get
            {
                return PeekLine() == null;
            }
        }

        public string PeekLine()
        {
            if (_currentLineNumber == _lines.Count)
                return null;

            return _lines[_currentLineNumber];
        }
    }
}