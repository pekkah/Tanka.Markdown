namespace Tanka.Markdown.Gist
{
    using System;
    using Blocks;

    public class GistBuilder : IBlockBuilder
    {
        public bool CanBuild(int start, StringRange content)
        {
            throw new NotImplementedException();
        }

        public Block Build(int start, StringRange content, out int end)
        {
            // sample link to gist: https://gist.github.com/pekkah/8304465
            int userNameStart = content.IndexOf(".com/") + 5;
            int gistIdStart = content.IndexOf('/', userNameStart) + 1;

            end = content.EndOfLine(gistIdStart, true);

            return new GistBlock(
                content,
                start,
                content.EndOfLine(start),
                new StringRange(
                    content,
                    userNameStart,
                    gistIdStart-1),
                new StringRange(
                    content,
                    gistIdStart,
                    content.EndOfLine(gistIdStart)));
        }
    }
}