namespace Tanka.Markdown
{
    using System;

    public class Heading : Block
    {
        public int Level { get; private set; }

        public string Text { get; private set; }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return true;
        }

        public override void End(string currentLine)
        {
            int hashesEnd = currentLine.LastIndexOf('#') + 1;
            string hashes = currentLine.Substring(0, hashesEnd);
            Level = hashes.Length <= 6 ? hashes.Length : 6;
            Text = currentLine.Substring(hashesEnd).Trim();
        }

        public override void AddLine(string currentLine)
        {
            throw new NotImplementedException();
        }
    }
}