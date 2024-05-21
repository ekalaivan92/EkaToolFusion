using System.Text;
using System.Xml;
using EkaToolFusion.Services.Extensions;
using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.Utilities;

public static class SnippetUtility
{
    public static string Generate(SnippetInputPayload payload)
    {
        var xml = new XmlDocument();

        var rootNode = xml.AddElement("CodeSnippets", namespaceURI: "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet");

        var xmlDeclaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        xml.InsertBefore(xmlDeclaration, rootNode);

        var codeSnippetNode = rootNode.AddElement("CodeSnippet");
        codeSnippetNode.AddAttribute("Format", "1.0.0");

        var headerNode = GenerateHeader(payload.Header);
        headerNode = xml.ImportNode(headerNode, true);
        codeSnippetNode.AppendChild(headerNode);

        var currentSnippetNode = codeSnippetNode.AddElement("Snippet");
        var referencesNode = GenerateReferences(payload.Body.References);
        referencesNode = xml.ImportNode(referencesNode, true);
        currentSnippetNode.AppendChild(referencesNode);

        var importsNode = GenerateImports(payload.Body.Imports);
        importsNode = xml.ImportNode(importsNode, true);
        currentSnippetNode.AppendChild(importsNode);

        var declarationsNode = GenerateDeclarations(payload.Body.Declarations);
        declarationsNode = xml.ImportNode(declarationsNode, true);
        currentSnippetNode.AppendChild(declarationsNode);

        var codeNode = GenerateCode(payload.Body.CodeBlock);
        codeNode = xml.ImportNode(codeNode, true);
        currentSnippetNode.AppendChild(codeNode);

        return xml.Format();
    }

    private static XmlNode GenerateHeader(SnippetInputHeaderPayload payload)
    {
        var headerDoc = new XmlDocument();

        headerDoc.AddElement("Header");
        headerDoc.AddElement("Title", payload.Title);
        headerDoc.AddElement("Author", payload.Author ?? "EkaSnippetGenerator");
        headerDoc.AddElement("Description", payload.Description);
        headerDoc.AddElement("HelpURL", payload.HelpURL);
        headerDoc.AddElement("Shortcut", payload.Shortcut);

        headerDoc
            .AddElement("SnippetTypes")
            .AddElement("SnippetType", payload.SnipperType.ToString());

        var keywordsElement = headerDoc
                .AddElement("Keywords");

        foreach (var keyword in payload.Keywords ?? [])
        {
            keywordsElement.AddElement("Keyword", keyword);
        }

        return headerDoc.DocumentElement;
    }

    private static XmlNode GenerateReferences(IEnumerable<SnippetReferenceInputPayload> payloads)
    {
        var referencesDoc = new XmlDocument();
        referencesDoc.AddElement("References");

        foreach (var reference in payloads)
        {
            var currentrefNode = referencesDoc.AddElement("Reference");
            currentrefNode.AddElement("Assembly", reference.Assembly);
            currentrefNode.AddElement("Url", reference.HelpURL);
        }

        return referencesDoc.DocumentElement;
    }

    private static XmlNode GenerateImports(IEnumerable<SnippetImportInputPayload> payloads)
    {
        var referencesDoc = new XmlDocument();
        referencesDoc.AddElement("Imports");

        foreach (var reference in payloads)
        {
            var currentrefNode = referencesDoc.AddElement("Import");
            currentrefNode.AddElement("Namespace", reference.Namespace);
        }

        return referencesDoc.DocumentElement;
    }

    private static XmlNode GenerateDeclarations(IEnumerable<SnippetDeclarationInputPayload> payloads)
    {
        var declarationsDoc = new XmlDocument();
        declarationsDoc.AddElement("Declarations");

        foreach (var reference in payloads)
        {
            var currentLitNode = declarationsDoc.AddElement("Literal");
            currentLitNode.AddAttribute("Editable", reference.Editable.ToString().ToLower());

            currentLitNode.AddElement("ID", reference.ID);
            currentLitNode.AddElement("Tooltip", reference.Tooltip);
            currentLitNode.AddElement("Default", reference.Default);
            currentLitNode.AddElement("Function", reference.Function);
        }

        return declarationsDoc.DocumentElement;
    }

    private static XmlNode GenerateCode(SnippetCodeInputPayload payload)
    {
        var code = $@"<![CDATA[
            {payload.Code}
        ]]>";

        var declarationsDoc = new XmlDocument();
        var codeNode = declarationsDoc.AddElement("Code");
        codeNode.InnerXml = code;
        codeNode.AddAttribute("Language", payload.Language);
        codeNode.AddAttribute("Kind", payload.Kind);
        codeNode.AddAttribute("Delimiter", payload.Delimiter);

        return declarationsDoc.DocumentElement;
    }

}