namespace Tanka.Markdown.Blocks
{
    public class ParagraphFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(currentLine))
                return false;

            return true;
        }

        public override BlockBuilderBase Create()
        {
            return new ParagraphBuilder();
        }
    }
}