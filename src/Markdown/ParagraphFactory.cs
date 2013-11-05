namespace Tanka.Markdown
{
    using System;

    public class ParagraphFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrEmpty(currentLine))
                throw new ArgumentNullException("currentLine");

            if (!string.IsNullOrEmpty(nextLine))
            {
                /***********************************
                * Setext Heading
                * ===============
                * 
                * Setext Heading
                * --------------
                *********************************/
                if (IsSetextHeadingMarker(nextLine))
                    return false;
            }

            // paragraph cannot start with a space
            if (currentLine.StartsWith(" "))
                return false;

            if (IsSetextHeadingMarker(currentLine))
                return false;

            return true;
        }

        private bool IsSetextHeadingMarker(string line)
        {
            if (line.StartsWith("==") || line.StartsWith("--"))
                return true;

            return false;
        }

        public override Block Create()
        {
            return new Paragraph();
        }
    }
}