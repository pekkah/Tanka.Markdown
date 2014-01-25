namespace Tanka.Markdown.Html
{
    using System.Text;
    using Blocks;
    using HtmlTags;

    public class OrderedListRenderer : BlockRendererBase<List>
    {
        protected override bool CanRender(List block)
        {
            return block.IsOrdered;
        }

        protected override HtmlTag Render(Document document, List block)
        {
            var ol = new HtmlTag("ol");

            foreach (Item item in block.Items)
            {
                var li = new HtmlTag("li", ol);
                var rawItemText = item.ToString().Replace("\r\n", "");
                var itemTextBuilder = new StringBuilder();

                bool previousWasSpace = false;
                foreach (var c in rawItemText)
                {
                    if (c == ' ')
                    {
                        if (previousWasSpace)
                            continue;

                        previousWasSpace = true;
                    }
                    else
                    {
                        previousWasSpace = false;
                    }

                    itemTextBuilder.Append(c);
                }

                li.Text(itemTextBuilder.ToString());
            }

            return ol;
        }
    }
}