namespace Tanka.Markdown
{
    using System;

    public class Heading : Block
    {
        public int Level { get; protected set; }

        public string Text { get; protected set; }

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return true;
        }

        public override bool End(string currentLine)
        {
            int hashesEnd = currentLine.LastIndexOf('#') + 1;
            string hashes = currentLine.Substring(0, hashesEnd);
            Level = hashes.Length <= 6 ? hashes.Length : 6;
            Text = currentLine.Substring(hashesEnd).Trim();

            return false;
        }

        public override void AddLine(string currentLine)
        {
            throw new NotImplementedException();
        }
    }
}