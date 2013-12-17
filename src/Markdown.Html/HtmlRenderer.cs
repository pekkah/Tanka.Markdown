namespace Tanka.Markdown.Html
{
    using System;
    using System.IO;
    using System.Reflection;
    using RazorEngine;

    public class HtmlRenderer
    {
        public string Render(Document document)
        {
            if (document == null) throw new ArgumentNullException("document");

            string template = GetTemplate();
            return Razor.Parse(template, document);
        }

        private string GetTemplate()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Tanka.Markdown.Html.document.cshtml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}