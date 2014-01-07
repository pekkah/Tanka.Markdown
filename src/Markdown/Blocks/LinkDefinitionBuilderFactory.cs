namespace Tanka.Markdown.Blocks
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;

    public class LinkDefinitionBuilderFactory : BlockFactoryBase
    {
        private readonly Regex _regex;

        public LinkDefinitionBuilderFactory()
        {
            _regex = VerbalExpressions.DefaultExpression
                .Maybe(" ")
                .Then("[")
                .Anything()
                .Then("]:")
                .Anything()
                .WithAnyCase()
                .ToRegex();
        }

        public override bool IsMatch(string currentLine, string nextLine)
        {
            var isMatch =  _regex.IsMatch(currentLine);

            return isMatch;
        }

        public override BlockBuilderBase Create()
        {
            return new LinkDefinitionBuilder();
        }
    }
}