# Markdown parser

# Features

* Parse markdown into a document
* Render markdown document as html

# Supported Blocks

* [Headings](http://www.heikura.me/tankamarkdown-headings)
* [Codeblocks](http://www.heikura.me/tankamarkdown-codeblocks-and-lists)
* [Lists](http://www.heikura.me/tankamarkdown-codeblocks-and-lists)
* Paragraphs
* Link definitions

# Usage

At your own risk :)

```
Install-Package Tanka.Markdown.Html -Pre
```

```
var parser = new MarkdownParser();
var renderer = new HtmlRenderer();

// parse markdown into a document 
Document document = parser.Parse(markdown);

// render document as html
string html = renderer.Render(document);
```
