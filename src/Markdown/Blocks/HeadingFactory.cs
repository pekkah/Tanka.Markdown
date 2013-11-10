namespace Tanka.Markdown.Blocks
{
    public class HeadingFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (currentLine.StartsWith("#"))
            {
                return true;
            }

            return false;
        }

        public override BlockBuilderBase Create()
        {
            return new HeadingBuilder();
        }
    }
}