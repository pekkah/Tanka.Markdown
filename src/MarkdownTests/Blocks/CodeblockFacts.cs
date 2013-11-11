namespace Tanka.MarkdownTests.Blocks
{
    using FluentAssertions;
    using Markdown.Blocks;
    using Xunit;

    public class CodeblockFacts
    {
        [Fact]
        public void StartsWithGitHubFlavoredCodeblockMarker()
        {
            string firstLine = "``` csharp";

            new CodeblockBuilderFactory()
                .IsMatch(firstLine, string.Empty)
                .ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ParseLanguage()
        {
            string firstLine = "``` csharp";

            var builder = new CodeblockBuilder();
            builder.AddLine(firstLine);

            var block = builder.Create() as Codeblock;
            block.Language.ShouldBeEquivalentTo("csharp");
        }

        [Fact]
        public void ParseIfNoLanguageGiven()
        {
            string firstLine = "```";

            var builder = new CodeblockBuilder();
            builder.AddLine(firstLine);

            var block = builder.Create() as Codeblock;
            block.Language.ShouldBeEquivalentTo(null);
        }

        [Fact]
        public void AfterEndShouldSkipLine()
        {
            var builder = new CodeblockBuilder();

            builder.End().ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void ParseCode()
        {
            string line = "public string Hello = \"world\";";

            var builder = new CodeblockBuilder();
            builder.AddLine(line);

            var block = builder.Create() as Codeblock;
            block.Code.ShouldBeEquivalentTo(line);
        }

        [Fact]
        public void BlockEndsAtEndMarker()
        {
            string line = "```";

            var builder = new CodeblockBuilder();
            bool isEnd = builder.IsEndLine("some code here", line);

            isEnd.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void Generic()
        {
            var lines = new[]
            {
                "``` generic",
                "public int x = 0;",
                "```"
            };

            var builder = new CodeblockBuilder();
            builder.AddLine(lines[0]);
            builder.AddLine(lines[1]);
            builder.AddLine(lines[2]);

            var block = builder.Create() as Codeblock;
            block.Code.ShouldBeEquivalentTo(lines[1]);
            block.Language.ShouldAllBeEquivalentTo("generic");
        }
    }
}