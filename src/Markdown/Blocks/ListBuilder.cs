namespace Tanka.Markdown.Blocks
{
    public abstract class ListBuilder : IBlockBuilder
    {
        public abstract bool CanBuild(int start, StringRange content);

        public abstract Block Build(int start, StringRange content, out int end);

        protected int FindEndOfItem(StringRange content, int position)
        {
            /*********************************************
             * item will end when the next line is not 
             * indented by at least one space
             * ******************************************/

            for (int possibleEnd = position; possibleEnd < content.End; possibleEnd++)
            {
                possibleEnd = content.EndOfLine(possibleEnd, true);
                var lineStart = content.StartOfNextLine(possibleEnd);

                if (lineStart == -1)
                {
                    return content.EndOfLine(possibleEnd);
                }

                // if line does not start with space then last line was the end of item
                if (!content.HasCharactersAt(lineStart, ' '))
                {
                    return content.EndOfLine(possibleEnd);
                }
            }

            return position;
        }
    }
}