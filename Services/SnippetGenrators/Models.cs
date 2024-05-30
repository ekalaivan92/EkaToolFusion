using System.Text.Json.Serialization;

namespace EkaToolFusion.Services.SnippetGenrator.Models;

public enum PredefinedSnippetType
{
    Bool = 1,
    Byte = 2,
    SByte = 3,
    Char = 4,
    Decimal = 5,
    Double = 6,
    Float = 7,
    Int = 8,
    uInt = 9,
    nInt = 10,
    nUint = 11,
    Long = 12,
    // ulong = 13,
    // Short = 14,
    // uShort = 15,
    NewtonSoftJson = 16,
    ByteArray = 17,

}

public enum SnippetType
{
    Expansion = 1,
    SourroundWith = 2
}

public class SnippetInputHeaderPayload
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string HelpURL { get; set; }
    public SnippetType SnipperType { get; set; }
    public string[] Keywords { get; set; }
    public string Shortcut { get; set; }

    [JsonIgnore]
    public string KeywordsForDisplay
    {
        get => string.Join(",", Keywords ?? []);
        set => Keywords = value.Split(",")
        .Select(x => x.Trim())
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .ToArray();
    }
}

public class SnippetReferenceInputPayload
{
    public string Assembly { get; set; }
    public string HelpURL { get; set; }
}

public class SnippetImportInputPayload
{
    public string Namespace { get; set; }
}

public class SnippetDeclarationInputPayload
{
    public string ID { get; set; }
    public string Function { get; set; }
    public string Default { get; set; }
    public string Tooltip { get; set; }
    public bool Editable { get; set; }
}

public class SnippetCodeInputPayload
{
    public string Language { get; set; }
    public string Kind { get; set; } = "cData";
    public string Delimiter { get; set; } = "$";
    public string Code { get; set; }
}


public class SnippetInputBodyPayload
{
    public bool ReferencesRequired { get; set; }
    public bool ImportsRequired { get; set; }
    public bool DeclarationsRequired { get; set; }

    public IEnumerable<SnippetReferenceInputPayload> References { get; set; }
    public IEnumerable<SnippetImportInputPayload> Imports { get; set; }
    public IEnumerable<SnippetDeclarationInputPayload> Declarations { get; set; }
    public SnippetCodeInputPayload CodeBlock { get; set; }

    public SnippetInputBodyPayload()
    {
        References = [];
        Imports = [];
        Declarations = [];
        CodeBlock = new();
    }
}

public class SnippetInputPayload
{
    public SnippetInputHeaderPayload Header { get; set; }
    public SnippetInputBodyPayload Body { get; set; }

    public SnippetInputPayload()
    {
        Header = new();
        Body = new();
    }
}