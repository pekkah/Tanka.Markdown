namespace Tanka.Markdown
{
    public class Normalize : IPreprocessor
    {
        public string Process(string markdown)
        {
            // normalize new lines to \n
            markdown = NewLines(markdown);

            return markdown;
        }

        private string NewLines(string markdown)
        {
            return markdown.Replace("\r\n", "\n");
        }
    }
}