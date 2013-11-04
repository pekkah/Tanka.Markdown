namespace Tanka.Markdown
{
    public abstract class Block
    {
        public abstract bool IsEndLine(string currentLine, string nextLine);

        public abstract void End(string currentLine);

        public abstract void AddLine(string currentLine);
    }
}