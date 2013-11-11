namespace Tanka.Markdown.Blocks
{
    public class Paragraph : Block
    {
        public Paragraph(string content)
        {
            Content = content;
        }

        public string Content { get; private set; }
    }
}