using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload UIntProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "UInt Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates uint property with auto implemented accessors.",
                    Keywords = ["uint", "Property"],
                    Shortcut = "mkuintprop",
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
                        Code = @"public uint $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}