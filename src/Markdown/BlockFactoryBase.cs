namespace Tanka.Markdown
{
    public abstract class BlockFactoryBase
    {
        public abstract bool IsMatch(string currentLine, string nextLine);

        public abstract BlockBuilderBase Create();
    }
}