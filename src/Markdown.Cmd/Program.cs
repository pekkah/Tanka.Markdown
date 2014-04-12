namespace Markdown.Cmd
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Tanka.Markdown;
    using Tanka.Markdown.Html;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowHelp();
            }

            var fileName = args[0];

            if (!File.Exists(fileName))
            {
                ShowHelp();
                return;
            }

            var content = File.ReadAllText(fileName);

            var parser = new MarkdownParser();
            var renderer = new MarkdownHtmlRenderer();

            try
            {
                Console.WriteLine("Starting parsing of {0}", fileName);
                var stopwatch = Stopwatch.StartNew();
                var document = parser.Parse(content);
                stopwatch.Stop();
                Console.WriteLine("Finished parsing in {0} seconds", stopwatch.Elapsed.TotalSeconds);

                stopwatch.Reset();
                stopwatch.Start();
                var html = renderer.Render(document);
                stopwatch.Stop();
                Console.WriteLine("Finished rendering in {0} seconds", stopwatch.Elapsed.TotalSeconds);

                var outputFile = Path.GetFileNameWithoutExtension(fileName) + ".html";
                Console.WriteLine("Writing output to {0}", outputFile);
                File.WriteAllText(outputFile, html);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
            }

            Console.ReadKey();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: mdhtml [markdownfile]");
        }
    }
}