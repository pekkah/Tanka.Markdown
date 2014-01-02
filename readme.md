# Markdown parser

# Features
* Parse markdown into a document
* Render markdown document as html

# Progress
* Document parsing mainly done
* Work on html renderer started

NOTE: Not ready for any serious use!!

# Usage

```
var parser = new MarkdownParser();
var renderer = new HtmlRenderer();

// parse markdown into a document 
Document document = parser.Parse(markdown);

// render document as html
string html = renderer.Render(document);
```