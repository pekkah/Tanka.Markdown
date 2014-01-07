namespace Tanka.MarkdownTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;
    using Markdown.Text;
    using Xbehave;

    public class ExtendByAddingNewSpan : MarkdownParserFactsBase
    {
        [Scenario]
        public void TwitterUserNameSpan()
        {
            var builder = new StringBuilder();
            builder.AppendLine("some random text here and a twitter user name @pekkath plus more text");

            "Given markdown parser with UserNameSpanFactory"
                .Given(() =>
                {
                    MarkdownParserOptions options = MarkdownParserOptions.Defaults;

                    // we first have to detect the @ token
                    options.Tokenizer.TokenFactories.Insert(
                        0,
                        new TokenFactory(str => str.StartsWith("@"), start => new Token("@", start)));

                    // this will actuall read the user name from the content
                    options.SpanFactories.Insert(0, new UserNameFactory());

                    var parser = new MarkdownParser(options);

                    GivenMarkdownParser(parser);
                    GivenTheMarkdown(builder.ToString());
                });

            "When markdown is parsed"
                .When(WhenTheMarkdownIsParsed);

            "Then count of document children should be 1"
                .Then(() => ThenDocumentChildrenShouldHaveCount(1));

            "And child at index 0 should be a paragraph with UserName span"
                .And(() => ThenDocumentChildAtIndexShould<Paragraph>(0, paragraph =>
                {
                    UserNameFactory.UserName userName = paragraph.Content.OfType<UserNameFactory.UserName>().Single();
                    userName.Name.ShouldBeEquivalentTo("@pekkath");
                }));
        }
    }

    public class UserNameFactory : SpanFactoryBase
    {
        public override bool IsMatch(IEnumerable<Token> tokens)
        {
            return tokens.First().Type == "@";
        }

        public override ISpan Create(Stack<Token> tokens, string content)
        {
            Token start = tokens.Pop();

            // todo(pekka): hmm this seems shady?
            int endOfUserName = content.IndexOf(" ", start.StartPosition, StringComparison.Ordinal);

            string name = GetTokenContent(start.StartPosition, endOfUserName, content);

            // HACK: as the tokenizer does not yet detect space we have to fix the next
            // token to have correct start position after the username has ended
            Token next = tokens.Peek();
            next.StartPosition = endOfUserName;

            return new UserName
            {
                Name = name
            };
        }

        public class UserName : ISpan
        {
            public string Name { get; set; }
        }
    }
}