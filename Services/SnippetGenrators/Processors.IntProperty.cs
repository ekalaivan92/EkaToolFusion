using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload IntProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Int Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates integer / int property with auto implemented accessors.",
                    Keywords = ["Int", "Int32", "Property"],
                    Shortcut = "mkintprop",
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
                        Code = @"public int $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}