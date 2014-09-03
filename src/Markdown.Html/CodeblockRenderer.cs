namespace Tanka.Markdown.Html
{
    using Blocks;
    using HtmlTags;

    public class CodeblockRenderer : BlockRendererBase<Codeblock>
    {
        protected override HtmlTag Render(Document document, Codeblock block)
        {
            var tag = new HtmlTag("pre");

            var code = new HtmlTag("code", tag);
            //code.Text(block.Code);
            code.Text(block.ToString());
            var syntax = block.Syntax;
            if (!string.IsNullOrWhiteSpace(syntax))
            {
                code.AddClass(string.Concat("lang-", syntax));
            }

            return tag;
        }
    }
}