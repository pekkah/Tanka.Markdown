namespace Tanka.Markdown.Blocks
{
    using System;

    public class LinkDefinitionBuilder : BlockBuilderBase
    {
        private string _key;
        private string _url;

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            return true;
        }

        public override bool End()
        {
            return false;
        }

        public override void AddLine(string currentLine)
        {
            // sample: [123]: http://test.com
            var line = currentLine.Trim();
            int lastIndexOfKey = line.LastIndexOf("]:", StringComparison.Ordinal);
            _key = line.Substring(1, lastIndexOfKey - 1);
            _url = line.Substring(lastIndexOfKey + 2).Trim();
        }

        public override Block Create()
        {
            return new LinkDefinition(_key, _url);
        }
    }
}