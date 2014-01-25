namespace Tanka.Markdown.Blocks
{
    using System.Collections.Generic;
    using Markdown;

    public class LinkDefinitionList : Block
    {
        public LinkDefinitionList(
            StringRange parent,
            int start,
            int end,
            IEnumerable<LinkDefinition> definitions) : base(parent, start, end)
        {
            Definitions = definitions;
        }

        public IEnumerable<LinkDefinition> Definitions { get; private set; }
    }
}