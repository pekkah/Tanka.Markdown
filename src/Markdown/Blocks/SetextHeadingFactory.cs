namespace Tanka.Markdown.Blocks
{
    public class SetextHeadingOneFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(nextLine))
                return false;

            if (nextLine.StartsWith("=="))
                return true;

            if (nextLine.StartsWith("--"))
                return true;

            return false;
        }

        public override BlockBuilderBase Create()
        {
            return new SetextHeadingBuilder();
        }
    }
}