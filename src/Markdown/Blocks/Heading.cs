namespace Tanka.Markdown.Blocks
{
    public class Heading : Block
    {
        public Heading(int level, string text)
        {
            Level = level;
            Text = text;
        }

        public int Level { get; protected set; }

        public string Text { get; protected set; }
    }

    public class HeadingBuilder : BlockBuilderBase
    {
        private int _level;
        private string _text;

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return true;
        }

        public override bool End()
        {
            return false;
        }

        public override void AddLine(string currentLine)
        {
            int hashesEnd = currentLine.LastIndexOf('#') + 1;
            string hashes = currentLine.Substring(0, hashesEnd);
            _level = hashes.Length <= 6 ? hashes.Length : 6;
            _text = currentLine.Substring(hashesEnd).Trim();
        }

        public override Block Create()
        {
            return new Heading(_level, _text);
        }
    }
}