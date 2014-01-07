namespace Tanka.Markdown.Text
{
    public interface ITokenFactory
    {
        bool CanCreate(string text);
        Token Create(int start);
    }
}