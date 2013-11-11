namespace Tanka.Markdown.Blocks
{
    public class Blockquote : Block
    {
        public Blockquote(Document document)
        {
            Document = document;
        }

        public Document Document { get; protected set; }
    }
}