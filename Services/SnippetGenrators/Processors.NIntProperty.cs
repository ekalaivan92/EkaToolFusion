using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload NIntProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "NInt Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates nint property with auto implemented accessors.",
                    Keywords = ["nint", "Property"],
                    Shortcut = "mknintprop",
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
                        Code = @"public nint $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}