namespace Tanka.Markdown.Blocks
{
    public class SetextHeadingBuilder : BlockBuilderBase
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

        public override bool End()
        {
            return false;
        }

        public override void AddLine(string currentLine)
        {
            if (currentLine.StartsWith("=="))
            {
                _level = 1;
                return;
            }

            if (currentLine.StartsWith("--"))
            {
                _level = 2;
                return;
            }

            // must be the actual heading
            _text = currentLine.Trim();
        }

        public override Block Create()
        {
            return new Heading(_level, _text);
        }
    }
}