using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Serialization;
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

    public static SnippetInputPayload ParseSnippet(MemoryStream xmlStream)
    {
        var payload = new SnippetInputPayload() { Body = new() };
        var xml = new XmlDocument();
        var nsmgr = new XmlNamespaceManager(xml.NameTable);
        nsmgr.AddNamespace("ns", "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet");

        xmlStream.Position = 0;
        xml.Load(xmlStream);

        payload.Header = ParseHeader(xml.GetElement("//ns:Header", nsmgr), nsmgr);
        payload.Body.References = ParseReferences(xml.GetElements("//ns:References", nsmgr), nsmgr);
        payload.Body.Imports = ParseImports(xml.GetElements("//ns:Imports", nsmgr), nsmgr);
        payload.Body.Declarations = ParseDeclarations(xml.GetElements("//ns:Declarations", nsmgr), nsmgr);

        payload.Body.ReferencesRequired = payload.Body.References.Count() > 0;
        payload.Body.ImportsRequired = payload.Body.Imports.Count() > 0;
        payload.Body.DeclarationsRequired = payload.Body.Declarations.Count() > 0;

        var codeBlockElement = xml.SelectSingleNode("//ns:Code", nsmgr);
        payload.Body.CodeBlock = new SnippetCodeInputPayload
        {
            Code = codeBlockElement?.InnerText.Trim(),
            Delimiter = codeBlockElement.GetAttributeValue("Delimiter"),
            Language = codeBlockElement.GetAttributeValue("Language"),
            Kind = codeBlockElement.GetAttributeValue("Kind"),
        };

        return payload;
    }

    private static SnippetInputHeaderPayload ParseHeader(XmlNode xmlNode, XmlNamespaceManager nsmgr)
    {
        Enum.TryParse(typeof(SnippetType), xmlNode.GetElement("//ns:SnippetTypes/ns:SnippetType", nsmgr)?.InnerText, out object snippetType);

        var keyWords = new List<string>();
        var keywordElements = xmlNode.GetElements("//ns:Keywords", nsmgr);

        foreach (XmlNode keywordNode in keywordElements)
        {
            keyWords.Add(keywordNode.InnerText);
        }

        var payload = new SnippetInputHeaderPayload
        {
            Title = xmlNode.GetElement("//ns:Title", nsmgr)?.InnerText,
            Author = xmlNode.GetElement("//ns:Author", nsmgr)?.InnerText ?? "EkaSnippetGenerator",
            Description = xmlNode.GetElement("//ns:Description", nsmgr)?.InnerText,
            HelpURL = xmlNode.GetElement("//ns:HelpURL", nsmgr)?.InnerText,
            Shortcut = xmlNode.GetElement("//ns:Shortcut", nsmgr)?.InnerText,
            SnipperType = (SnippetType)snippetType,
            Keywords = [.. keyWords]
        };

        return payload;
    }

    private static IEnumerable<SnippetReferenceInputPayload> ParseReferences(XmlNodeList xmlNode, XmlNamespaceManager nsmgr)
    {
        var result = new List<SnippetReferenceInputPayload>();

        if (xmlNode == null || xmlNode.Count == 0)
        {
            return result;
        }

        foreach (XmlNode refNode in xmlNode)
        {
            if (refNode == null || refNode.ChildNodes.Count == 0)
            {
                continue;
            }

            var refInputPayload = new SnippetReferenceInputPayload
            {
                Assembly = refNode.GetElement("//ns:Assembly", nsmgr)?.InnerText,
                HelpURL = refNode.GetElement("//ns:Url", nsmgr)?.InnerText
            };

            result.Add(refInputPayload);
        }

        return result;
    }

    private static IEnumerable<SnippetImportInputPayload> ParseImports(XmlNodeList xmlNode, XmlNamespaceManager nsmgr)
    {
        var result = new List<SnippetImportInputPayload>();

        if (xmlNode == null || xmlNode.Count == 0)
        {
            return result;
        }

        foreach (XmlNode refNode in xmlNode)
        {
            if (refNode == null || refNode.ChildNodes.Count == 0)
            {
                continue;
            }

            var refInputPayload = new SnippetImportInputPayload
            {
                Namespace = refNode.GetElement("//ns:Namespace", nsmgr)?.InnerText
            };

            result.Add(refInputPayload);
        }

        return result;
    }

    private static IEnumerable<SnippetDeclarationInputPayload> ParseDeclarations(XmlNodeList xmlNode, XmlNamespaceManager nsmgr)
    {
        var result = new List<SnippetDeclarationInputPayload>();

        if (xmlNode == null || xmlNode.Count == 0)
        {
            return result;
        }

        foreach (XmlNode refNode in xmlNode)
        {
            if (refNode == null || refNode.ChildNodes.Count == 0)
            {
                continue;
            }

            var refInputPayload = new SnippetDeclarationInputPayload
            {
                ID = refNode.GetElement("//ns:Literal/ns:ID", nsmgr)?.InnerText,
                Default = refNode.GetElement("//ns:Literal/ns:Default", nsmgr)?.InnerText,
                Function = refNode.GetElement("//ns:Literal/ns:Function", nsmgr)?.InnerText,
                Tooltip = refNode.GetElement("//ns:Literal/ns:Tooltip", nsmgr)?.InnerText,
                Editable = bool.Parse(refNode.GetAttributeValue("Editable") ?? "false")
            };

            result.Add(refInputPayload);
        }

        return result;
    }
}