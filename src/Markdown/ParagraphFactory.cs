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
                *********************************/
                if (nextLine.StartsWith("=="))
                    return false;
            }

            // paragraph cannot start with a space
            if (currentLine.StartsWith(" "))
                return false;

            return true;
        }

        public override Block Create()
        {
            return new Paragraph();
        }
    }
}