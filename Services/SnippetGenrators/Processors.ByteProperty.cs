using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload ByteProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Byte Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates byte property with auto implemented accessors.",
                    Keywords = ["Byte", "Property"],
                    Shortcut = "mkbyteprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "C#",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public byte $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}