namespace Tanka.Markdown.Blocks
{
    public class Codeblock : Block
    {
        public Codeblock(string language, string code)
        {
            Language = language;
            Code = code;
        }

        public string Language { get; private set; }

        public string Code { get; private set; }
    }
}