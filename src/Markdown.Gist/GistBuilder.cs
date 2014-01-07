namespace Tanka.Markdown.Gist
{
    using System;

    public class GistBuilder : BlockBuilderBase
    {
        private string _gistId;
        private string _userName;

        public override bool IsEndLine(string currentLine, string nextLine)
        {
            // gist is single line block
            return true;
        }

        public override bool End()
        {
            // no op
            return false;
        }

        public override void AddLine(string currentLine)
        {
            if (currentLine == null) throw new ArgumentNullException("currentLine");

            // sample link to gist: https://gist.github.com/pekkah/8304465
            int userNameStart = currentLine.LastIndexOf(".com/") + 5;
            int gistIdStart = currentLine.LastIndexOf('/') + 1;

            _userName = currentLine.Substring(userNameStart, currentLine.Length - gistIdStart - 1);
            _gistId = currentLine.Substring(gistIdStart);
        }

        public override Block Create()
        {
            return new GistBlock(_userName, _gistId);
        }
    }
}