using System.Text.Json.Serialization;

namespace EkaToolFusion.Services.SnippetGenrator.Models;

public enum PredefinedSnippetType
{
    Int = 1,
    NewtonSoftJson = 2,

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
    public string KeywordsForDisplay { 
        get => string.Join(",",  Keywords ?? []); 
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