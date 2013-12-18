namespace Tanka.Markdown.Blocks
{
    public class EmptyLineFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(currentLine))
                return true;

            return false;
        }

        public override BlockBuilderBase Create()
        {
            return new EmptyLineBuilder();
        }
    }

    public class EmptyLineBuilder : BlockBuilderBase
    {
        public override bool IsEndLine(string currentLine, string nextLine)
        {
            if (!string.IsNullOrEmpty(nextLine))
                return true;

            return false;
        }

        public override bool End()
        {
            return false;
        }

        public override void AddLine(string currentLine)
        {
        }

        public override Block Create()
        {
            return new EmptyLine();
        }
    }

    public class EmptyLine : Block
    {
    }
}