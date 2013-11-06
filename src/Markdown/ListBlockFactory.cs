namespace Tanka.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ListBlockFactory : BlockFactoryBase
    {
        public static List<Func<string, bool>> IsListItem = new List<Func<string, bool>>
        {
            item => item.StartsWith("* "),
            item => item.StartsWith("- "),
            item =>
            {
                int indexOfDot = item.IndexOf('.');
                if (indexOfDot < 0)
                    return false;

                string numberStr = item.Substring(0, indexOfDot);
                int _ = 0;

                return int.TryParse(numberStr, out _);
            }
        };

        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (string.IsNullOrWhiteSpace(currentLine))
                return false;

            return IsListItem.Any(f => f(currentLine));
        }

        public override BlockBuilder Create()
        {
            return new ListBlockBuilder();
        }
    }
}