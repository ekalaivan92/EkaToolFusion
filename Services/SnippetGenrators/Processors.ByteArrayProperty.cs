using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload ByteArrayProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Byte Array Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates byte[] property with auto implemented accessors.",
                    Keywords = ["Byte Array", "Property"],
                    Shortcut = "mkbytearrayprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    DeclarationsRequired = true,
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "csharp",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public byte[] $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}