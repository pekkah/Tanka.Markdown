namespace Tanka.Markdown
{
    public class BlockquoteFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (currentLine.StartsWith("> "))
                return true;

            return false;
        }

        public override BlockBuilder Create()
        {
            return new BlockquoteBuilder();
        }
    }
}