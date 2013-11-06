namespace Tanka.Markdown
{
    using System;
    using System.Net.Mime;

    public class SetextHeadingOneFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(nextLine))
                return false;

            if (nextLine.StartsWith("=="))
                return true;

            if (nextLine.StartsWith("--"))
                return true;

            return false;
        }

        public override BlockBuilder Create()
        {
            return new SetextHeadingBuilder();
        }
    }

    public class SetextHeadingBuilder : BlockBuilder
    {
        private int _level;
        private string _text;

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (currentLine.StartsWith("=="))
                return true;

            if (currentLine.StartsWith("--"))
                return true;

            return false;
        }

        public override bool End(string currentLine)
        {
            if (currentLine.StartsWith("=="))
                _level = 1;

            if (currentLine.StartsWith("--"))
                _level = 2;

            return false;
        }

        public override void AddLine(string currentLine)
        {
            _text = currentLine.Trim();
        }

        public override Block Create()
        {
            return new Heading(_level, _text);
        }
    }
}