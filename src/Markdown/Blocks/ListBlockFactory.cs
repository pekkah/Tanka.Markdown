namespace Tanka.Markdown.Blocks
{
    public class UnorderedListFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(currentLine))
                return false;

            if (currentLine.StartsWith("* "))
                return true;

            if (currentLine.StartsWith("- "))
                return true;

            return false;
        }

        public override BlockBuilderBase Create()
        {
            return new ListBlockBuilder(ListStyle.Unordered);
        }
    }

    public class OrderedListFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(currentLine))
                return false;

            int indexOfDot = currentLine.IndexOf('.');
            if (indexOfDot < 0)
                return false;

            string numberStr = currentLine.Substring(0, indexOfDot);
            int _ = 0;

            return int.TryParse(numberStr, out _);
        }

        public override BlockBuilderBase Create()
        {
            return new ListBlockBuilder(ListStyle.Ordered);
        }
    }
}