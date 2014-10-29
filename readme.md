# Markdown parser

![Build](https://ci.appveyor.com/api/projects/status/github/pekkah/Tanka.Markdown)

[![Build status](https://ci.appveyor.com/api/projects/status/github/pekkah/Tanka.Markdown?svg=true)](https://ci.appveyor.com/project/pekkah/tanka-markdown)

# Features

* Parse markdown into a document structure
* Render markdown document as html

# Supported Syntax

* [Headings](http://www.heikura.me/tankamarkdown-headings)
* [Codeblocks](http://www.heikura.me/tankamarkdown-codeblocks-and-lists)
* [Lists](http://www.heikura.me/tankamarkdown-codeblocks-and-lists)
* [Paragraphs](https://www.heikura.me/tankamarkdown-paragraphs)
* Link definitions

# Extensiblity

* See Tanka.Markdown.Gist

# Usage

At your own risk, but I'm also dogfooding it myself in my
own blog engine available at https://github.com/pekkah/tanka. Live 
version at http://www.heikura.me.

```
Install-Package Tanka.Markdown.Html
```

```
var parser = new MarkdownParser();
var renderer = new HtmlRenderer();

// parse markdown into a document 
Document document = parser.Parse(markdown);

// render document as html
string html = renderer.Render(document);
```
