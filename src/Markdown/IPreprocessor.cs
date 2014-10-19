namespace Tanka.Markdown
{
    public interface IPreprocessor
    {
        string Process(string markdown);
    }
}