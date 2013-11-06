namespace Tanka.Markdown
{
    using System;

    public class Heading : Block
    {
        public int Level { get; protected set; }

        public string Text { get; protected set; }

        public Heading(int level, string text)
        {
            Level = level;
            Text = text;
        }
    }

    public class HeadingBuilder : BlockBuilder
    {
        private int _level;
        private string _text;

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return true;
        }

        public override bool End(string currentLine)
        {
            int hashesEnd = currentLine.LastIndexOf('#') + 1;
            string hashes = currentLine.Substring(0, hashesEnd);
            _level = hashes.Length <= 6 ? hashes.Length : 6;
            _text = currentLine.Substring(hashesEnd).Trim();

            return false;
        }

        public override void AddLine(string currentLine)
        {
            throw new NotImplementedException();
        }

        public override Block Create()
        {
            return new Heading(_level, _text);
        }
    }
}