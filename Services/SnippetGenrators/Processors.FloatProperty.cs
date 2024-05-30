using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload FloatProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Float Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates float property with auto implemented accessors.",
                    Keywords = ["float", "Property"],
                    Shortcut = "mkfloatprop",
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
                        Code = @"public float $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}