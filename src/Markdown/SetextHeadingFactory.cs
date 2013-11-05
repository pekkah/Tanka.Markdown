namespace Tanka.Markdown
{
    public class SetextHeadingOneFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(nextLine))
                return false;

            if (nextLine.StartsWith("=="))
                return true;

            return false;
        }

        public override Block Create()
        {
            return new SetextHeadingOne();
        }
    }

    public class SetextHeadingOne : Heading
    {
        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return nextLine.StartsWith("==");
        }

        public override bool End(string currentLine)
        {
            Level = 1;
            Text = currentLine.Trim();

            return true;
        }
    }

    public class SetextHeadingTwoFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(nextLine))
                return false;

            if (nextLine.StartsWith("--"))
                return true;

            return false;
        }

        public override Block Create()
        {
            return new SetextHeadingTwo();
        }
    }

    public class SetextHeadingTwo : Heading
    {
        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return nextLine.StartsWith("--");
        }

        public override bool End(string currentLine)
        {
            Level = 2;
            Text = currentLine.Trim();

            return true;
        }
    }
}