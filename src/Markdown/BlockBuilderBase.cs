namespace Tanka.Markdown
{
    public abstract class BlockBuilderBase
    {
        public abstract bool IsEndLine(string currentLine, string nextLine);

        public abstract bool End();

        public abstract void AddLine(string currentLine);

        public abstract Block Create();
    }
}