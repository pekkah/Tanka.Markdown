namespace Tanka.Markdown.Gist
{
    public class GistFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(currentLine))
                return false;

            return currentLine.StartsWith("https://gist.github.com/");
        }

        public override BlockBuilderBase Create()
        {
            return new GistBuilder();
        }
    }
}