namespace Tanka.Markdown
{
    public abstract class BlockBuilder
    {
        public abstract bool IsEndLine(string currentLine, string nextLine);

        public abstract bool End(string currentLine);

        public abstract void AddLine(string currentLine);

        public abstract Block Create();
    }

    public abstract class Block
    {
        
    }
}