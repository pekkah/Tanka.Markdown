namespace Tanka.Markdown.Text
{
    using System;

    public class TokenFactory : ITokenFactory
    {
        private readonly Func<string, bool> _canCreate;
        private readonly Func<int, Token> _create;

        public TokenFactory(Func<string, bool> canCreate, Func<int, Token> create)
        {
            if (canCreate == null) throw new ArgumentNullException("canCreate");
            if (create == null) throw new ArgumentNullException("create");

            _canCreate = canCreate;
            _create = create;
        }

        public bool CanCreate(string text)
        {
            return _canCreate(text);
        }

        public Token Create(int start)
        {
            return _create(start);
        }
    }
}