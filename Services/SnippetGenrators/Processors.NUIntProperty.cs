using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload NUIntProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "NUInt Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates nuint property with auto implemented accessors.",
                    Keywords = ["nuint", "Property"],
                    Shortcut = "mknuintprop",
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
                        Code = @"public nuint $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}