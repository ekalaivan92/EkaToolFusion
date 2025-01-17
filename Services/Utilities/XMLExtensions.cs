using System.Text;
using System.Xml;

namespace EkaToolFusion.Services.Extensions;

public static class XMLExtensions
{
    public static XmlNode AddElement(this XmlDocument xmlDoc, string localName, string content = "", string prefix = "", string namespaceURI = "")
    {
        var title = xmlDoc.CreateElement(prefix, localName, namespaceURI);
        if (!string.IsNullOrEmpty(content))
        {
            title.InnerText = content;
        }
        if (xmlDoc.DocumentElement != null)
        {
            return xmlDoc.DocumentElement.AppendChild(title);
        }
        else
        {
            return xmlDoc.AppendChild(title);
        }
    }

    public static XmlNode AddElement(this XmlNode xmlDoc, string localName, string content = "", string prefix = "", string namespaceURI = "")
    {
        var title = xmlDoc.OwnerDocument.CreateElement(prefix, localName, namespaceURI);
        if (!string.IsNullOrEmpty(content))
        {
            title.InnerText = content.Trim();
        }

        return xmlDoc.AppendChild(title);
    }

    public static XmlNode GetElement(this XmlNode xmlDoc, string path, XmlNamespaceManager nsmgr)
    {
        var list = GetElements(xmlDoc, path, nsmgr)[0];
        return list.FirstChild;
    }

    public static XmlNodeList GetElements(this XmlNode xmlDoc, string path, XmlNamespaceManager nsmgr)
    {
        return xmlDoc.SelectNodes(path, nsmgr);
    }

    public static XmlNode AddAttribute(this XmlNode xmlDoc, string attributeName, string value)
    {
        var attr = xmlDoc.OwnerDocument.CreateAttribute(attributeName);
        attr.Value = value;
        xmlDoc.Attributes.Append(attr);

        return xmlDoc;
    }

    public static string GetAttributeValue(this XmlNode xmlDoc, string attributeName)
    {
        return xmlDoc.Attributes[attributeName]?.Value;
    }

    public static string Format(this XmlDocument doc)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            NewLineOnAttributes = false,
            Encoding = new UTF8Encoding(false),
            OmitXmlDeclaration = false  // Ensure XML declaration is included
        };

        using var memoryStream = new MemoryStream();
        using var writer = XmlWriter.Create(memoryStream, settings);
        doc.Save(writer);
        writer.Flush();

        return Encoding.UTF8.GetString(memoryStream.ToArray()).Trim().Replace("xmlns=\"\"", "");
    }
}