using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Authorization;

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

    public static XmlNode AddAttribute(this XmlNode xmlDoc, string attributeName, string value)
    {
        var attr = xmlDoc.OwnerDocument.CreateAttribute(attributeName);
        attr.Value = value;
        xmlDoc.Attributes.Append(attr);

        return xmlDoc;
    }

    public static string Format(this XmlDocument doc)
    {
        var stringWriter = new StringWriter(new StringBuilder());
        var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
        doc.Save(xmlTextWriter);
        
        return stringWriter.ToString();
    }
}